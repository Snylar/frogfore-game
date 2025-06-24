using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskUIManager : MonoBehaviour
{
    public TaskManager taskManager;

    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotContainerParent;
    private GameObject gameManagerObject;

    private void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        taskManager = gameManagerObject.GetComponent<TaskManager>();
    }

    public void UpdateTaskManager()
{
    // Clear the item slots container
    foreach (Transform taskSlot in itemSlotContainerParent)
    {
        Destroy(taskSlot.gameObject);
    }

    // Create new item slots and assign sprites
    foreach (Task task in taskManager.tasks)
    {
        RectTransform taskSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

        TextMeshProUGUI taskText = taskSlot.Find("Task Name").GetComponent<TextMeshProUGUI>();
        taskText.text = task.title;
        TextMeshProUGUI taskDesc = taskSlot.Find("Task Description").GetComponent<TextMeshProUGUI>();
        taskDesc.text = task.subtitle;
        Toggle toggleButton = taskSlot.Find("Task Toggle").GetComponent<Toggle>();
        toggleButton.isOn = task.taskComplete;
    }
}
}
