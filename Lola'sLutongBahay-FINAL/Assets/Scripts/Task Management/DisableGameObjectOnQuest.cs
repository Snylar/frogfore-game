using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObjectOnQuest : MonoBehaviour
{
    [Header("Quest Settings")]
    [SerializeField] private string taskName;
    [SerializeField] private GameObject objectToDisable;

    private TaskManager taskManager;

    void Start()
    {
        taskManager = TaskManager.instance;

        if (taskManager == null)
        {
            Debug.LogError("TaskManager instance not found! Make sure it's in the scene.");
            return;
        }

        CheckQuestStatus();
    }

    public void CheckQuestStatus()
    {
        if (taskManager == null)
        {
            Debug.LogWarning("TaskManager is missing — cannot check quest status.");
            return;
        }

        if (taskManager.IsTaskComplete(taskName))
        {
            DisableObject();
        }
    }

    public void DisableObject()
    {
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
            Debug.Log($"{objectToDisable.name} has been disabled.");
        }
        else
        {
            Debug.LogWarning("No GameObject assigned to disable!");
        }
    }
}