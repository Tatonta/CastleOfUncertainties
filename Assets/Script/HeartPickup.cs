using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    [SerializeField]
    PlayerCombat player;
    int heal = 30;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            if (player.playerCurrentHealth + heal > player.playerMaxHealth)
                player.playerCurrentHealth = player.playerMaxHealth;
            else
                player.playerCurrentHealth = player.playerCurrentHealth + heal;
            HealthBar.getInstance().SetHealth(player.playerCurrentHealth);
            ScoreManager.getInstance().ScoreNumber(20);
        }
    }
}
