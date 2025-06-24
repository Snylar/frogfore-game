using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class DialogStep
{
    public string dialogName;
    public string dialogText;
    public Sprite reactionImage;
}

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI dialogName;
    public Image reactionImage;

    public DialogStep[] dialogSteps;
    private int currentStepIndex = 0;

    private string currentDialog;
    private int currentCharIndex = 0;
    private bool isAnimatingText = false;

    public float textAnimationSpeed = 30f;
    [SerializeField] UnityEvent actionEvent;

    private void Start()
    {
        dialogText.text = "";
        UpdateDialogStep();
    }

    private void UpdateDialogStep()
    {
        if (currentStepIndex < dialogSteps.Length)
        {
            DialogStep currentStep = dialogSteps[currentStepIndex];

            dialogName.text = currentStep.dialogName;
            currentDialog = currentStep.dialogText;
            reactionImage.sprite = currentStep.reactionImage;

            dialogPanel.SetActive(true);

            StartCoroutine(AnimateText());
        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }

    private IEnumerator AnimateText()
    {
        isAnimatingText = true;
        currentCharIndex = 0;
        dialogText.text = "";

        while (currentCharIndex < currentDialog.Length)
        {
            dialogText.text += currentDialog[currentCharIndex];
            currentCharIndex++;
            yield return new WaitForSeconds(1f / textAnimationSpeed);
        }

        isAnimatingText = false;
    }

    private void Update()
    {
        if (isAnimatingText)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StopCoroutine(AnimateText());
                dialogText.text = currentDialog;
                isAnimatingText = false;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (currentStepIndex < dialogSteps.Length - 1)
            {
                currentStepIndex++;
                UpdateDialogStep();
            }
            else
            {
                dialogPanel.SetActive(false);
                actionEvent.Invoke();
            }
        }
    }
}
