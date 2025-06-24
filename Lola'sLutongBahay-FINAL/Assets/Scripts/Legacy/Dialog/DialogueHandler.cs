using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public enum NPCType
{
    Normal,
    QuestGiver,
    QuestTarget
}

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueHandler : MonoBehaviour
{
    [Header("NPC Settings")]
    public NPCType npcType = NPCType.Normal;

    [Header("Dialogue Settings")]
    public GameObject DialoguePanel;
    public bool autoTriggerOnCollision = false;
    public Dialogue initialDialogue;
    public Dialogue afterQuestDialogue;
    private bool playerInRange = false;

    [Header("Quest Integration")]
    [SerializeField] private string taskName;
    [SerializeField] private UnityEvent onQuestComplete;

    [Header("Quest Indicator")]
    public GameObject exclamationMark; // Appears if quest hasn't been activated

    [Header("Events")]
    public UnityEvent onDialogueComplete;

    private TaskManager taskManager;
    private bool hasGivenQuest = false;
    private bool hasSeenInitialDialogue = false;

    void Start()
    {
        taskManager = TaskManager.instance;
        if (taskManager == null)
        {
            Debug.LogError("TaskManager instance not found! Ensure TaskManager is in the scene.");
        }

        UpdateExclamationMark();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerInRange)
        {
            TriggerDialogue();
            DialoguePanel.SetActive(false);
        }
    }

    public void TriggerDialogue()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.dialogBoxOpened = true;
        }
        else
        {
            Debug.LogWarning("GameManager instance not found!");
        }

        Dialogue currentDialogue = GetCurrentDialogue();
        if (DialogueController.Instance != null)
        {
            DialogueController.Instance.StartDialogue(currentDialogue);
        }
        else
        {
            Debug.LogWarning("DialogueController instance not found!");
        }

        if (npcType == NPCType.QuestGiver && !hasGivenQuest)
        {
            AssignQuest();
        }

        if (npcType == NPCType.QuestTarget)
        {
            CompleteQuest();
        }

        if (!hasSeenInitialDialogue)
        {
            hasSeenInitialDialogue = true; // Mark initial dialogue as "seen"
        }

        Invoke(nameof(EndDialogue), 2f);
    }

    private Dialogue GetCurrentDialogue()
    {
        bool isTaskComplete = taskManager != null && taskManager.IsTaskComplete(taskName);

        if (!hasSeenInitialDialogue)
        {
            return initialDialogue; // First time speaking
        }
        else if (isTaskComplete)
        {
            return afterQuestDialogue; // After quest is completed
        }
        else
        {
            return null; // No dialogue after the first talk until quest is done
        }
    }

    private void AssignQuest()
    {
        if (taskManager == null || string.IsNullOrEmpty(taskName) || hasGivenQuest) return;

        taskManager.AddTask(taskName);
        QuestHUD.instance?.UpdateQuestHUD();
        hasGivenQuest = true;
        UpdateExclamationMark();
        Debug.Log($"Quest assigned: {taskName}");
    }

    private void CompleteQuest()
    {
        if (taskManager == null || string.IsNullOrEmpty(taskName)) return;

        Task task = taskManager.GetTask(taskName);
        if (task != null && !task.taskComplete)
        {
            task.MarkAsComplete();
            QuestHUD.instance?.UpdateQuestHUD();
            onQuestComplete?.Invoke();
            UpdateExclamationMark();
            Debug.Log($"Quest '{taskName}' completed.");
        }
    }

    private void UpdateExclamationMark()
    {
        if (exclamationMark == null) return;

        bool hasActiveTask = taskManager != null && taskManager.GetTask(taskName) != null;
        bool isTaskComplete = taskManager != null && taskManager.IsTaskComplete(taskName);

        if (npcType == NPCType.QuestGiver && !hasGivenQuest)
        {
            exclamationMark.SetActive(true); // Show until quest is accepted
        }
        else if (npcType == NPCType.QuestTarget && hasActiveTask && !isTaskComplete)
        {
            exclamationMark.SetActive(true); // Show until quest is complete
        }
        else
        {
            exclamationMark.SetActive(false); // Hide otherwise
        }
    }

    public void EndDialogue()
    {
        Debug.Log("Dialogue finished!");
        onDialogueComplete?.Invoke();

        if (GameManager.instance != null)
        {
            GameManager.instance.dialogBoxOpened = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            DialoguePanel?.SetActive(true);

            if (autoTriggerOnCollision)
            {
                TriggerDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            DialoguePanel?.SetActive(false);
        }
    }
}