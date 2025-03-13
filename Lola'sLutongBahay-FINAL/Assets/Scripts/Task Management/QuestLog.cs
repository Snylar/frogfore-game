using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLog : MonoBehaviour
{
    private TaskManager taskManager;

    void Start()
    {
        taskManager = TaskManager.instance;

        if (taskManager == null)
        {
            Debug.LogWarning("TaskManager instance not found! Ensure TaskManager is in the scene.");
            return;
        }
    }

    public void AddQuest(string questTitle)
    {
        taskManager.AddTask(questTitle);
    }

    public void CompleteQuest(string questTitle)
    {
        taskManager.MarkTaskAsComplete(questTitle);
    }

    public List<Task> GetActiveQuests()
    {
        return taskManager.tasks.FindAll(task => !task.taskComplete);
    }
}