using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text hudText;
    private static ScoreManager instance;
    public static int score = 0;

    public void Awake()
    {
        instance = this;
    }

    public void ScoreNumber(int points)
    {
        score += points;
        hudText.text = score.ToString();
        PlayerPrefs.SetInt("USER_SCORE", score);
    }

    public static ScoreManager getInstance()
    {
        return instance;
    }

}
