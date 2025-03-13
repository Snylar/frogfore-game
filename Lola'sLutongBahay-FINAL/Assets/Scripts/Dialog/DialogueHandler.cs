using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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
    [SerializeField] private string taskName; // The task this NPC gives or completes
    [SerializeField] private string targetNPCName; // For QuestGivers: the NPC you must talk to
    [SerializeField] private UnityEvent onQuestComplete;

    [Header("Events")]
    public UnityEvent onDialogueComplete; // New event for when dialogue ends

    private TaskManager taskManager;

    void Start()
    {
        taskManager = TaskManager.instance;
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
        GameManager.instance.dialogBoxOpened = true;
        DialogueController.Instance.StartDialogue(dialogue);

        if (npcType == NPCType.QuestGiver)
        {
            AssignTalkToNPCQuest();
        }
        else if (npcType == NPCType.QuestTarget)
        {
            CompleteTalkToNPCQuest();
        }

        // Automatically call EndDialogue when the conversation finishes
        Invoke(nameof(EndDialogue), 2f); // Adjust timing based on your dialogue flow
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
    }

    public void EndDialogue()
    {
        Debug.Log("Dialogue finished!");
        onDialogueComplete?.Invoke(); // Triggers UnityEvents for anything tied to the end of dialogue
        GameManager.instance.dialogBoxOpened = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialoguePanel.SetActive(true);
            StarttheDialogue = true;

            if (npcType == NPCType.QuestGiver)
            {
                Debug.Log("Quest Giver NPC detected.");
            }
            else if (npcType == NPCType.QuestTarget)
            {
                Debug.Log("Quest Target NPC detected.");
            }
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