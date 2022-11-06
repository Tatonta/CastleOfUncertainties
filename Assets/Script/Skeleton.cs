using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distToPlayer < aggroRange && gameObject.layer != 11 && player.layer != 11) //Se non è morto
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



    private void ChasePlayer()
    {

        animator.SetBool("IsWalking", true);
        if (InRange() && Time.time >= nextAttackTime)
        {
            Attacking = true;
            Debug.Log("Set Trigger Attacco Nemico");
            animator.SetTrigger("Attack");
            animator.SetBool("IsWalking", false);
        }
        else if (!InRange())
        {
            if (transform.position.x < (player.transform.position.x - castDist) && !Attacking)
            { // Si sposta verso destra
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                transform.localScale = new Vector2(1, 1);
                isFacingLeft = false;
            }
            else if (transform.position.x > (player.transform.position.x - castDist) && !Attacking)// Si sposta verso Sinistra
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                transform.localScale = new Vector2(-1, 1);
                isFacingLeft = true;
            }
        }
    }


}
