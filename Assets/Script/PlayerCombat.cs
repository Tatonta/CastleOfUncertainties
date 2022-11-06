using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour, IDamageable<int>, IPlayer
{
    public Animator playerAnimator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float heavyAttackRange = 0.8f;
    public LayerMask enemyLayers;
    public GameObject Blood;
    public GameMaster gm;
    public bool Flaming = false;
    public GameObject Flames;
    public Transform flaming_spot;

    [SerializeField]
    public int playerMaxHealth = 100;

    public int playerCurrentHealth;

    public int attackDamage = 40;
    public int heavyAttackDamage = 50;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        HealthBar.getInstance().SetMaxHealth(playerMaxHealth);
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Flaming)
            Instantiate(Flames, flaming_spot.position, Quaternion.identity);
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1.5f / attackRate;
            }
            else if(Input.GetMouseButton(1))
            {
                AttackHeavy();
                nextAttackTime = Time.time + 3f / attackRate;
            }
        }
    }

    public void Attack()
    {

        // play an attack animation
        playerAnimator.SetTrigger("Attack");

        // detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    public void AttackHeavy()
    {

        playerAnimator.SetTrigger("Heavy_Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(heavyAttackDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        playerCurrentHealth -= damage;
        playerAnimator.SetTrigger("Hurt");
        HealthBar.getInstance().SetHealth(playerCurrentHealth);

        Instantiate(Blood, transform.position, Quaternion.identity);

        if (playerCurrentHealth <= 0)
        {
            Die();
        }
        
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Die()
    {
        Debug.Log("Sei Morto!");
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CoinManager.getInstance().SaveCoins();
        playerAnimator.SetTrigger("Died");
        gameObject.layer = 11;
    }
}
