using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputName : MonoBehaviour
{

        public string userName;
        public InputField inputField;

        public void SetUserName(string text)
        {
            Debug.Log(text);
            userName = text;
            PlayerPrefs.SetString("playerName", userName);
            SceneManager.LoadScene("EndScreen");
        }
}
