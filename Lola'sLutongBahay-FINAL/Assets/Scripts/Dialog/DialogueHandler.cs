using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public enum NPCType
{
    Normal,
    QuestGiver,
    QuestTarget // NPC you need to talk to for a quest
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
    public Dialogue dialogue;
    public bool StarttheDialogue;

    [Header("Quest Integration")]
    [SerializeField] private string taskName; // Task this NPC gives or completes
    [SerializeField] private string targetNPCName; // For QuestGivers: the NPC you must talk to
    [SerializeField] private UnityEvent onQuestComplete;

    [Header("Events")]
    public UnityEvent onDialogueComplete; // Called when dialogue ends

    private TaskManager taskManager;

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
        if (Input.GetKeyDown(KeyCode.F) && StarttheDialogue)
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

        if (DialogueController.Instance != null)
        {
            DialogueController.Instance.StartDialogue(dialogue);
        }
        else
        {
            Debug.LogWarning("DialogueController instance not found!");
        }

        if (npcType == NPCType.QuestGiver)
        {
            AssignTalkToNPCQuest();
        }
        else if (npcType == NPCType.QuestTarget)
        {
            CompleteTalkToNPCQuest();
        }

        // EndDialogue after dialogue finishes (adjust timing as needed)
        Invoke(nameof(EndDialogue), 2f);
    }

    private void AssignTalkToNPCQuest()
    {
        if (taskManager == null || string.IsNullOrEmpty(taskName) || string.IsNullOrEmpty(targetNPCName)) return;

        taskManager.AddTask(taskName);
        Debug.Log($"Quest assigned: Talk to {targetNPCName}");
    }

    private void CompleteTalkToNPCQuest()
    {
        if (taskManager == null || string.IsNullOrEmpty(taskName)) return;

        Task task = taskManager.GetTask(taskName);
        if (task != null && !task.taskComplete)
        {
            task.MarkAsComplete();
            onQuestComplete?.Invoke();
            Debug.Log($"Quest '{taskName}' completed by talking to {gameObject.name}.");
        }
        else if (task == null)
        {
            Debug.LogWarning($"Task '{taskName}' not found in TaskManager.");
        }
    }

    public void EndDialogue()
    {
        Debug.Log("Dialogue finished!");
        onDialogueComplete?.Invoke(); // Triggers UnityEvents tied to end of dialogue

        if (GameManager.instance != null)
        {
            GameManager.instance.dialogBoxOpened = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialoguePanel.SetActive(true);
            StarttheDialogue = true;

            Debug.Log(npcType == NPCType.QuestGiver ? "Quest Giver NPC detected." : "Quest Target NPC detected.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialoguePanel.SetActive(false);
            StarttheDialogue = false;
        }
    }
}
