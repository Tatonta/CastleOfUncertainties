using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    public static double delay = 1.5;
    private bool CanLaser = false;
    private float shootRange = 7;

    private float fireRate;
    public float nextFire;

    [SerializeField]
    GameObject bullet;

    void Start()
    {
        fireRate = 2f;
        nextFire = Time.time;
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distToPlayer < shootRange && gameObject.layer != 11 && player.layer != 11) //Se non è morto
        {
            if (!Attacking && distToPlayer < aggroRange) //
            {
               Debug.Log("Chasing");
               ChasePlayer();
            }
            else
            {
                Debug.Log("Spara");
                animator.SetBool("IsWalking", false);
                if (Time.time > nextFire)
                {
                    Debug.Log("I'm shooting");
                    Instantiate(bullet, transform.position, Quaternion.identity);
                    nextFire = Time.time + fireRate;
                }
            }
        }
        else
        {
            StopChasingPlayer();
            animator.SetBool("IsWalking", false);

        }
    }


    private void ChasePlayer()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (InRange() && Time.time >= nextAttackTime && !blocking) //Attacca
        {
            CanLaser = true;
            Attacking = true;
            Debug.Log("Set Trigger Attacco Nemico");
            randomAttack();
            animator.SetBool("IsWalking", false);
            nextAttackTime = Time.time + attackRate;
        }
        else if (!InRange() && distToPlayer < aggroRange && !blocking) //Cammina
        {
            animator.SetBool("IsWalking", true);
            Debug.Log("!InRange() && distToPlayer < aggroRange && !blocking");
            blocking = false;
            animator.SetBool("Block", blocking);
            if (transform.position.x < (player.transform.position.x - castDist) && !Attacking)
            { // Si sposta verso destra
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                transform.localScale = new Vector2(2, 2);
                isFacingLeft = false;
            }
            else if (transform.position.x > (player.transform.position.x - castDist) && !Attacking)// Si sposta verso Sinistra
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                transform.localScale = new Vector2(-2, 2);
                isFacingLeft = true;
            }
        }
        //else if(!InRange() && distToPlayer < shootRange && distToPlayer > aggroRange && CanLaser) //Spara
        //{
            
        //}
    }

    void randomAttack()
    {
        int random = Random.Range(0, 100);

        if (random >= 0 && random <= 40)
            animator.SetTrigger("AttackA");
        else if (random >= 40 && random <= 80)
            animator.SetTrigger("AttackB");
        else if(random >= 80 && random <= 99)
         {
                blocking = true;
                animator.SetBool("Block", blocking);
         }


        Attacking = false;
        return;
    }

    
    void Block()
    {
        StartCoroutine(ImmuneToDamage());
        return;
    }

    IEnumerator ImmuneToDamage()
    {
        Debug.Log("Immune al danno");
        blocking = true;
        StopChasingPlayer();
        yield return new WaitForSeconds(0.8f);
        blocking = false;
        yield return null;
    }

    void Die()
    {
        blocking = false;
        animator.SetBool("Block", blocking);
        Debug.Log("Enemy died!");
        // Die animation

        ScoreManager.getInstance().ScoreNumber(scoreAmount);

        animator.SetTrigger("Death");

        rb2d.velocity = new Vector2(0, 0);
        //Disable the enemy

        gameObject.layer = 11;

    }
}
