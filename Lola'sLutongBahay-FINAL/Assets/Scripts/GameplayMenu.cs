using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayMenu : MonoBehaviour
{
    public static GameplayMenu instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI currentBitCoinText;
    [SerializeField] private Slider[] xpSlider;

    private PlayerStats[] playerStats;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    void Update()
    {
        if (GameManager.instance == null)
        {
            Debug.LogWarning("GameManager instance not found!");
            return; // Skip if GameManager is missing
        }

        UpdateBitcoinDisplay();
        UpdateXpDisplay();
    }

    private void UpdateBitcoinDisplay()
    {
        if (currentBitCoinText != null)
        {
            currentBitCoinText.text = "₱ " + GameManager.instance.currentBitcoins;
        }
        else
        {
            Debug.LogWarning("currentBitCoinText is not assigned in the inspector!");
        }
    }

    private void UpdateXpDisplay()
    {
        playerStats = GameManager.instance.GetPlayerStats();
        if (playerStats == null || playerStats.Length == 0)
        {
            Debug.LogWarning("PlayerStats not found or empty!");
            return;
        }

        if (xpSlider != null && xpSlider.Length > 0)
        {
            xpSlider[0].maxValue = playerStats[0].xpForNextLevel[playerStats[0].playerLevel];
            xpSlider[0].value = playerStats[0].currentXp;
        }
        else
        {
            Debug.LogWarning("xpSlider is not assigned or empty in the inspector!");
        }
    }
}