using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable<int>
{

    int currentHealth;
    [SerializeField]
    int maxHealth = 100;
    public Animator animator;
    public GameObject SkeletonFragments;
    public int scoreAmount;
    Rigidbody2D rb2d;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

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

            Debug.Log(currentHealth);

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
        
        Debug.Log("Enemy died!");
        // Die animation

        ScoreManager.getInstance().ScoreNumber(scoreAmount);

        animator.SetTrigger("Death");

        rb2d.velocity = new Vector2(0, 0);
        //Disable the enemy

        gameObject.layer = 11;
        
    }
}
