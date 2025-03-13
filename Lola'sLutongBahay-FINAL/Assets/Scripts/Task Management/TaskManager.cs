using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;
    public List<Task> tasks = new List<Task>();
    public UnityEvent onTaskUpdated;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Re-added AddTask to create new tasks and refresh the QuestHUD
    public void AddTask(string title, string subtitle = "")
    {
        Task newTask = new Task(title) { subtitle = subtitle };
        tasks.Add(newTask);
        onTaskUpdated?.Invoke(); // Notify HUD or other systems
        QuestHUD.instance?.UpdateQuestHUD();
        Debug.Log($"Task added: {title} with subtitle: {subtitle}");
    }
    public void AddTaskFromEvent(string title)
    {
    AddTask(title);
    }

    public Task GetTask(string title)
    {
        return tasks.Find(task => task.title == title);
    }

    public void MarkTaskAsComplete(string title)
    {
        Task task = GetTask(title);
        if (task != null && !task.taskComplete)
        {
            task.MarkAsComplete();
            onTaskUpdated?.Invoke(); // Notify listeners that a task was completed
            QuestHUD.instance?.UpdateQuestHUD();
            Debug.Log($"Task marked as complete: {title}");
        }
        else
        {
            Debug.LogWarning($"Task '{title}' not found or already completed.");
        }
    }

    public bool IsTaskComplete(string title)
    {
        Task task = GetTask(title);
        return task != null && task.taskComplete;
    }

    public List<Task> GetActiveTasks()
    {
        List<Task> activeTasks = new List<Task>();
        foreach (Task task in tasks)
        {
            if (!task.taskComplete)
            {
                activeTasks.Add(task);
            }
        }
        return activeTasks;
    }
}