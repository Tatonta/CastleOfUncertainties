using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int attackDamage;
    public float attackRange;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;
    private bool Attacking;
    public LayerMask playerLayer;
    [SerializeField]
    Transform[] movespots;

    [SerializeField]
    Transform attackPoint;
    [SerializeField]
    Transform castPoint;


    public Animator animator;
    [SerializeField]
    Transform player;

    [SerializeField]
    float aggroRange;

    [SerializeField]
    float moveSpeed;
    

    Rigidbody2D rb2d;

    public int maxHealth = 100;
    int currentHealth;
    private int randomSpot;
    private bool isFacingLeft;

    // Start is called before the first frame update
    void Start()
    {
        randomSpot = UnityEngine.Random.Range(0, movespots.Length);
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < aggroRange && gameObject.layer != 11) //Se non è morto
        {
            if (!Attacking)
            {
                ChasePlayer();
            }
        }
        else
        {
            StopChasingPlayer();
        }
    }
    
    
    bool InRange()
    {
        float castDist = 0.5f;
        if(isFacingLeft)
        {
            castDist *= -1;
        }
        Vector2 endPos = castPoint.position + new Vector3(castDist,0,0);
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));

        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                StopChasingPlayer();
                return true;
            }
            return false;
        }

        return false;
    }
    

    void StopChasingPlayer()
    {
        animator.SetBool("IsWalking", false);
        rb2d.velocity = Vector2.zero;
    }

    void ChasePlayer()
    {

            animator.SetBool("IsWalking", true);
            if (InRange())
            {
                AttackPlayer();
                animator.SetBool("IsWalking", false);
            }
            else if (transform.position.x < (player.position.x-1)) // Si sposta verso destra
            {
                rb2d.velocity = new Vector2(moveSpeed * Time.deltaTime, 0);
                transform.localScale = new Vector2(1, 1);
                isFacingLeft = false;
            }
            else // Si sposta verso Sinistra
            {
                rb2d.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0);
                transform.localScale = new Vector2(-1, 1);
                isFacingLeft = true;
            }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");
        // play hurt animation

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    bool AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            // play an attack animation
            animator.SetTrigger("Attack");

            // detect enemies in range of attack
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            //Damage them
            foreach (Collider2D player in hitPlayer)
            {
                player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
            }
            nextAttackTime = Time.time + 2f;
            return Attacking = true;
        }
        return Attacking = false;
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        // Die animation

        animator.SetBool("IsDead", true);

        rb2d.velocity = new Vector2(0, 0);
        //Disable the enemy

        gameObject.layer = 11;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

