using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed;

    private Transform head;
    private GameObject Player;
    private Vector2 target;
    private int projectileDamage = 5;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        head = GameObject.FindGameObjectWithTag("CeilingCheck").transform;
        Player = GameObject.FindGameObjectWithTag("Player");
        target = (head.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(target.x, target.y);
        Destroy(gameObject, 2f);
        Debug.Log(target);
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.GetComponent<PlayerCombat>().TakeDamage(projectileDamage);
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
