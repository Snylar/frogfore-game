using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string playerName;

    [SerializeField] int maxLevel = 50;
    public int playerLevel = 1;
    public int currentXp;
    public int[] xpForNextLevel;
    [SerializeField] int baseLevelXp = 100;

    public int playerGold;
    [SerializeField] int playerGem;
    // Start is called before the first frame update
    void Start()
    {
        xpForNextLevel = new int[maxLevel];
        xpForNextLevel[1] = baseLevelXp;

        for(int i = 2; i < xpForNextLevel.Length; i++)
        {
            xpForNextLevel[i] = (int)(0.02f * i * i * i + 3.06f * i * i + 105.6f * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            AddXp(100);
        }
    }

    public void AddXp(int amountOfXp)
    {
        currentXp += amountOfXp;
        if(currentXp > xpForNextLevel[playerLevel])
        {
            currentXp -= xpForNextLevel[playerLevel];
            playerLevel++;
        }
    }
    public void DestroyQuitGame()
    {
        // Add any additional cleanup or effects before destroying the GameObject
        Destroy(gameObject);
    }
}
