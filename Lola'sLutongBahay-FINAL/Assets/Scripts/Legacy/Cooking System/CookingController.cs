using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CookingController : MonoBehaviour
{
    public bool isAdded = false;
    public float cookingTime = 5f; // Adjust as needed
    private int CookFlipped;

    public Animator cookingAnimator;
    [SerializeField] string triggeranimation;
    public RecipeManager recipeManager;
    [SerializeField] string actionName;
    [SerializeField] string actionNameTwo;
    public UnityEvent EggFlipEvent;
    public UnityEvent CookingDoneEvent;

    public bool isCooking = false;

    private void Update()
    {
        if (isAdded && !isCooking)
        {
            StartCooking();
        }
    }

    public void StartCooking()
    {
        isCooking = true;
        StartCoroutine(CookingProcess());
        CookFlipped = CookFlipped + 1;
    }

    private IEnumerator CookingProcess()
    {
        float startTime = Time.time;

        while (Time.time - startTime < cookingTime)
        {
            // Simulate cooking process (could change sprite color, scale, etc.)
            yield return null;
        }

        // Cooking is done, trigger animation
        cookingAnimator.SetTrigger(triggeranimation);

        if (CookFlipped == 1)
        {
            EggFlipEvent.Invoke();
            recipeManager.playerActions.Add(actionNameTwo);
        }

        if (CookFlipped == 2)
        {
            CookingDoneEvent.Invoke();
            recipeManager.playerActions.Add(actionName);
        }
        isCooking = false;
        isAdded = false;
    }
}

