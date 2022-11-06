using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int coins = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Collisione con:" + collision.name);
            Destroy(gameObject);
            CoinManager.getInstance().AddCoin(coins);
            ScoreManager.getInstance().ScoreNumber(30);
        }
    }
}
