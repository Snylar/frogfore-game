using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    // Singleton instance
    private static CookingManager instance;

    // Private constructor to prevent instantiation from other scripts
    private CookingManager() { }
    [SerializeField] GameObject TutorialUI;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject VictoryUI;

    // Public static method to access the singleton instance
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static CookingManager Instance
    {
        get
        {
            // If the instance is null, find the existing CookingManager in the scene or create a new one if none exists
            if (instance == null)
            {
                instance = FindObjectOfType<CookingManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<CookingManager>();
                    singletonObject.name = "CookingManager";
                }
            }

            return instance;
        }
    }

    // Methods representing different states of the game
    public void Tutorial()
    {
        // Implement the tutorial logic here
    }

    public void Cooking()
    {
        // Implement the cooking logic here
    }

    public void GameOver()
    {
        GameOverUI.SetActive(true);
    }

    public void Victory()
    {
        VictoryUI.SetActive(true);
    }

    // Optional: You can add other methods or variables as needed for your game

    // Optional: Ensure the instance is not destroyed when loading a new scene
    // Make sure to comment this out if you want the CookingManager to reset when loading a new scene.
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}


