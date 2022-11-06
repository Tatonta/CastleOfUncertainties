using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 lastCheckPointPos;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        } else
        {
            Destroy(gameObject);
        }
    }

    public static GameMaster getInstance()
    {
        return instance;
    }

    public void SetCheckPoint(Vector2 checkpoint)
    {
        lastCheckPointPos = checkpoint;
    }
}
