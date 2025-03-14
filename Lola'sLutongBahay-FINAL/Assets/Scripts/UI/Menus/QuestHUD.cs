using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestHUD : MonoBehaviour
{
    public static QuestHUD instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI questListText;

    private TaskManager taskManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        taskManager = TaskManager.instance;
        if (taskManager == null)
        {
            Debug.LogError("TaskManager not found! Ensure TaskManager is in the scene.");
        }

        UpdateQuestHUD();
    }

    public void UpdateQuestHUD()
    {
        if (taskManager == null || questListText == null) return;

        questListText.text = ""; // Clear existing quest list

        foreach (Task task in taskManager.tasks)
        {
            if (!task.taskComplete)
            {
                questListText.text += $"- {task.title}: {task.subtitle}\n";
            }
        }
    }
}