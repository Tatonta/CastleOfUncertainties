using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePotionBuff : MonoBehaviour
{

    public PlayerCombat playerCombat;
    public Boss boss;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
            boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
            boss.attackDamage = 10;
            playerCombat.attackDamage = 100;
            playerCombat.heavyAttackDamage = 175;
            playerCombat.Flaming = true;
            Destroy(gameObject);
            ScoreManager.getInstance().ScoreNumber(100);
        }
    }
}
