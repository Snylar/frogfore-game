using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CookingController2 : MonoBehaviour
{
    public Animator cookingAnimator; // The Animator to connect to
    public float cookingTime = 10f; // Total cooking time in seconds
    public int triggerThreshold = 5; // Value at which the animator should trigger
    public float incrementInterval = 1f; // Time between each increment
    public GameObject cookingObject; // The GameObject to apply the sprites to

    public RecipeManager recipeManager;
    [SerializeField] string actionName;
    [SerializeField] UnityEvent actionEvent;
    [SerializeField] GameObject RestartButton;

    private float currentCookingTime = 0f;
    private int currentValue = 0;
    private bool isCooking = false;

    private void Update()
    {
        if (isCooking)
        {
            currentCookingTime += Time.deltaTime;

            // Increment the value and trigger the animator if needed
            if (currentCookingTime >= incrementInterval)
            {
                currentCookingTime = 0f;
                IncrementValue();

                // Check if cooking is done
                if (currentValue >= triggerThreshold)
                {
                    EndCooking();
                }
            }
        }
        if (currentValue == 16)
        {
            
        }
    }

    public void StartCooking()
    {
        if (!isCooking)
        {
            currentCookingTime = 0f;
            currentValue = 0;
            isCooking = true;
        }
    }

    private void IncrementValue()
    {
        currentValue++;
        // Connect to the animator and trigger if the threshold is reached
        if (cookingAnimator != null)
        {
            cookingAnimator.SetInteger("CookingState", currentValue);
            recipeManager.playerActions.Add(actionName);
            actionEvent.Invoke();
        }
    }

    private void EndCooking()
    {
        isCooking = false;
        Debug.Log("Cook Burnt!");
        RestartButton.SetActive(true);
    }
}
