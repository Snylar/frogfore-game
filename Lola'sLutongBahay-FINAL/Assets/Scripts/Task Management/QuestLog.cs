using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class QuestLog : MonoBehaviour
{
    private TaskManager taskManager;

    [Header("UI Elements")]
    public GameObject questLogPanel; // The quest log UI panel
    public TextMeshProUGUI activeQuestsText;
    public TextMeshProUGUI completedQuestsText;
    public Button questLogButton; // Assign "QuestLogButton" here

    [Header("Hotkey Settings")]
    public KeyCode toggleKey = KeyCode.Q;

    void Start()
    {
        taskManager = TaskManager.instance;

        if (taskManager == null)
        {
            Debug.LogWarning("TaskManager instance not found! Ensure TaskManager is in the scene.");
            return;
        }

        taskManager.onTaskUpdated.AddListener(UpdateQuestLog);

        if (questLogButton == null)
        {
            questLogButton = GameObject.Find("QuestLogButton")?.GetComponent<Button>();
            if (questLogButton == null)
            {
                Debug.LogError("QuestLogButton not found in the scene!");
                return;
            }
        }

        questLogButton.onClick.AddListener(ToggleQuestLog);
        UpdateQuestLog();
        questLogPanel.SetActive(false); // Hide the panel by default
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            questLogButton.onClick.Invoke(); // Simulate a button click using the hotkey
        }
    }

    public void ToggleQuestLog()
    {
        bool isActive = questLogPanel.activeSelf;
        questLogPanel.SetActive(!isActive);

        if (!isActive)
        {
            UpdateQuestLog();
        }
    }

    public void UpdateQuestLog()
    {
        if (taskManager == null) return;

        List<Task> activeTasks = taskManager.GetActiveTasks();
        List<Task> completedTasks = taskManager.tasks.FindAll(task => task.taskComplete);

        activeQuestsText.text = "Active Quests:\n";
        foreach (var task in activeTasks)
        {
            activeQuestsText.text += $"- {task.title}\n";
        }

        completedQuestsText.text = "Completed Quests:\n";
        foreach (var task in completedTasks)
        {
            completedQuestsText.text += $"- {task.title}\n";
        }
    }

    private void OnDestroy()
    {
        if (taskManager != null)
        {
            taskManager.onTaskUpdated.RemoveListener(UpdateQuestLog);
        }

        if (questLogButton != null)
        {
            questLogButton.onClick.RemoveListener(ToggleQuestLog);
        }
    }
}