using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjectOnQuest : MonoBehaviour
{
    [SerializeField] private string questName; // Quest to check
    [SerializeField] private GameObject objectToEnable; // Object to activate
    private bool isActivated = false;

    void Start()
    {
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(false); // Start disabled
        }
    }

    public void ActivateAfterDialogue()
    {
        if (isActivated || objectToEnable == null) return;

        TaskManager taskManager = TaskManager.instance;
        if (taskManager != null && taskManager.IsTaskComplete(questName))
        {
            objectToEnable.SetActive(true);
            Debug.Log($"Activated {objectToEnable.name} after dialogue for completed quest: {questName}");
            isActivated = true; // Prevents re-triggering
        }
        else
        {
            Debug.Log($"Quest '{questName}' is not complete, object won't activate.");
        }
    }
}