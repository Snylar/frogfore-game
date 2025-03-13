using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    [Header("Task Management")]
    public List<Task> tasks = new List<Task>();

    [Header("Events")]
    [SerializeField] private UnityEvent allTasksFinished;

    private bool hasInvokedAllTasksFinished = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Prevent duplicate TaskManagers
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject); // Persist TaskManager across scenes
    }

    private void Update()
    {
        if (AreAllTasksComplete() && !hasInvokedAllTasksFinished)
        {
            allTasksFinished?.Invoke();
            hasInvokedAllTasksFinished = true;
            Debug.Log("All tasks completed! Invoking final event.");
        }
    }

    public void AddTask(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            Debug.LogWarning("Cannot add a task with an empty title.");
            return;
        }

        if (GetTask(title) != null)
        {
            Debug.LogWarning($"Task '{title}' already exists!");
            return;
        }

        Task newTask = new Task(title);
        tasks.Add(newTask);
        Debug.Log($"Task added: {title}");
    }

    public Task GetTask(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            Debug.LogWarning("Task title is null or empty.");
            return null;
        }

        return tasks.Find(task => task.title == title);
    }

    public void MarkTaskAsComplete(string title)
    {
        Task task = GetTask(title);
        if (task != null && !task.taskComplete)
        {
            task.MarkAsComplete();
            Debug.Log($"Task marked as complete: {title}");
        }
        else if (task == null)
        {
            Debug.LogWarning($"Task '{title}' not found!");
        }
        else
        {
            Debug.LogWarning($"Task '{title}' is already complete.");
        }
    }

    public bool IsTaskComplete(string title)
    {
        Task task = GetTask(title);
        return task != null && task.taskComplete;
    }

    public bool AreAllTasksComplete()
    {
        if (tasks.Count == 0)
        {
            Debug.Log("No tasks available — treating as all tasks complete.");
            return true; // No tasks means everything is technically "done"
        }

        foreach (Task task in tasks)
        {
            if (!task.taskComplete)
            {
                return false;
            }
        }

        return true; // All tasks are complete
    }
}
