using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLoader : MonoBehaviour
{
    public GameObject gameManagerPrefab;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            GameObject gm = Instantiate(gameManagerPrefab);
            DontDestroyOnLoad(gm);
        }
    }
}
