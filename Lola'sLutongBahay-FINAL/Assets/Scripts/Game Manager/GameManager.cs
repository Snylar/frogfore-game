using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player Stats")]
    [SerializeField] private PlayerStats[] playerStats;

    [Header("Game State")]
    public bool gameMenuOpened;
    public bool dialogBoxOpened;
    public bool shopPanelOpened;

    [Header("Currency")]
    public int currentBitcoins;

    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else if (instance != this && gameObject != null)
        {
            Debug.LogWarning("Duplicate GameManager detected. Destroying the new one.");
            DestroyImmediate(gameObject); // Immediate destruction prevents lingering issues
        }
    }

    private void Update()
    {
        if (PlayerController.instance != null)
        {
            PlayerController.instance.deactivateMovement = gameMenuOpened || dialogBoxOpened || shopPanelOpened;
        }
    }

    public PlayerStats[] GetPlayerStats()
    {
        if (playerStats == null || playerStats.Length == 0)
        {
            Debug.LogWarning("PlayerStats array is empty or not assigned!");
        }
        return playerStats;
    }

    public void AddBitcoins(int amount)
    {
        currentBitcoins += amount;
        Debug.Log($"Added {amount} bitcoins. Current balance: {currentBitcoins}");
    }

    public void RemoveBitcoins(int amount)
    {
        currentBitcoins = Mathf.Max(currentBitcoins - amount, 0); 
        Debug.Log($"Removed {amount} bitcoins. Current balance: {currentBitcoins}");
    }

    public void DestroyQuitGame()
    {
        Debug.Log("Quitting game and destroying GameManager...");
        Destroy(gameObject);
    }
}