using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classifica : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    int counter = 1;


    void Start()
    {
        int score = PlayerPrefs.GetInt("USER_SCORE");
        PlayerPrefs.SetString("POSIZIONE_NOME_ATTUALE", PlayerPrefs.GetString("playerName"));
        PlayerPrefs.SetInt("PUNTEGGIO_ATTUALE", score);

        for (int i = 1; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("PUNTEGGIO_" + i.ToString()) < PlayerPrefs.GetInt("PUNTEGGIO_ATTUALE"))
            {
                int aux = PlayerPrefs.GetInt("PUNTEGGIO_" + i.ToString());
                string nomeAux = PlayerPrefs.GetString("POSIZIONE_NOME_" + i.ToString());

                PlayerPrefs.SetString("POSIZIONE_NOME_" + i.ToString(), PlayerPrefs.GetString("POSIZIONE_NOME_ATTUALE"));
                PlayerPrefs.SetInt("PUNTEGGIO_" + i.ToString(), PlayerPrefs.GetInt("PUNTEGGIO_ATTUALE"));

                PlayerPrefs.SetString("POSIZIONE_NOME_ATTUALE", nomeAux);
                PlayerPrefs.SetInt("PUNTEGGIO_ATTUALE", aux);
            }
        }

        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            canvas.transform.GetChild(i).GetComponent<Text>().text = PlayerPrefs.GetString("POSIZIONE_NOME_" + counter.ToString()) + ": " + PlayerPrefs.GetInt("PUNTEGGIO_" + counter.ToString());
            counter++;
        }
    }

}

