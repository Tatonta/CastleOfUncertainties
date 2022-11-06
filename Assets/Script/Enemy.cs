using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour, IDamageable<int>
{

    protected int currentHealth;
    [SerializeField]
    protected int maxHealth = 100;
    public GameObject SkeletonFragments;
    public int scoreAmount;



    public int attackDamage;
    public float attackRange;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;
    protected bool Attacking;
    public LayerMask playerLayer;
    public bool blocking;

    [SerializeField]
    protected Transform attackPoint;
    [SerializeField]
    protected Transform castPoint;


    public Animator animator;
    [SerializeField]
    protected GameObject player;

    [SerializeField]
    protected float aggroRange;

    [SerializeField]
    protected float moveSpeed;

    protected float castDist;

    protected Rigidbody2D rb2d;


    protected bool isFacingLeft;


    // Update is called once per frame

    
    IEnumerator AttackPlayer()
    {
        // Debug.Log("Attacco Nemico!");
            // play an attack animation
            //animator.SetTrigger("Attack");
            // detect enemies in range of attack
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            //Damage them
            if(hitPlayer != null)
                foreach (Collider2D player in hitPlayer)
                {
                    player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
                }
            nextAttackTime = Time.time + attackRate;
            Attacking = true;
            yield return new WaitForSeconds(0.5f);
        Attacking = false;
        yield return null;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    protected bool InRange()
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
    

    protected void StopChasingPlayer()
    {
        Debug.Log("Stop Chasing");
        animator.SetBool("IsWalking", false);
        rb2d.StopMoving();
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        if (gameObject.layer == 13 && GetComponent<Boss>().blocking)
        {
            return;
        }
        else
        {
            currentHealth -= damage;


            animator.SetTrigger("Hurt");
            // play hurt animation
            Instantiate(SkeletonFragments, transform.position, Quaternion.identity);
            if (currentHealth <= 0)
            {
                Die();
            }

        }
    }

    void Die()
    {

        // Die animation

        ScoreManager.getInstance().ScoreNumber(scoreAmount);

        animator.SetTrigger("Death");

        rb2d.velocity = new Vector2(0, 0);
        //Disable the enemy

        if(gameObject.layer == 13)
        {
            Debug.Log("Mammamia");
            SceneManager.LoadScene("EnterYourName");
        }

        gameObject.layer = 11;
    }
}

