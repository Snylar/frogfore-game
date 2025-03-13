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
    [SerializeField] private string targetNPCName;
    [SerializeField] private UnityEvent onQuestComplete;

    [Header("Events")]
    public UnityEvent onDialogueComplete;

    private TaskManager taskManager;
    private bool hasGivenQuest = false;
    private bool hasPlayedInitialDialogue = false; // NEW: Tracks if initial dialogue was shown

    void Start()
    {
        taskManager = TaskManager.instance;
        if (taskManager == null)
        {
            Debug.LogError("TaskManager instance not found! Ensure TaskManager is in the scene.");
        }
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
            AssignTalkToNPCQuest();
        }
        else if (npcType == NPCType.QuestTarget)
        {
            CompleteTalkToNPCQuest();
        }

        Invoke(nameof(EndDialogue), 2f);
    }

    private Dialogue GetCurrentDialogue()
    {
        if (!hasPlayedInitialDialogue)
        {
            hasPlayedInitialDialogue = true; // Ensure it only plays once
            return initialDialogue;
        }
        else if (taskManager != null && taskManager.IsTaskComplete(taskName))
        {
            return afterQuestDialogue;
        }
        return afterQuestDialogue; // Fallback if initial is done but quest isn't complete
    }

    private void AssignTalkToNPCQuest()
    {
        if (taskManager == null || string.IsNullOrEmpty(taskName) || hasGivenQuest) return;

        taskManager.AddTask(taskName);
        QuestHUD.instance?.UpdateQuestHUD();
        hasGivenQuest = true;
        Debug.Log($"Quest assigned: Talk to {targetNPCName}");
    }

    private void CompleteTalkToNPCQuest()
    {
        if (taskManager == null || string.IsNullOrEmpty(taskName)) return;

        Task task = taskManager.GetTask(taskName);
        if (task != null && !task.taskComplete)
        {
            task.MarkAsComplete();
            QuestHUD.instance?.UpdateQuestHUD();
            onQuestComplete?.Invoke();
            Debug.Log($"Quest '{taskName}' completed.");
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