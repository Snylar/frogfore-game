using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CookingController3 : MonoBehaviour
{
    [SerializeField] Animator animator;
    public float cookingTime = 10f; // Total cooking time in seconds
    public float animationInterval = 2f; // Interval at which to trigger animations
    public string[] animationTriggers; // Array of animation triggers
    public UnityEvent onCookingComplete; // Unity event to invoke when cooking is done

    public RecipeManager recipeManager;
    [SerializeField] Slider CookingStateSlider;


    private float currentTime = 0f;
    private int animationIndex = 0;
    private bool isCooking = false;

    private void Update()
    {
        if (isCooking)
        {
            currentTime += Time.deltaTime;

            // Check if it's time to trigger the next animation
            if (animationIndex < animationTriggers.Length && currentTime >= animationInterval)
            {
                TriggerAnimation(animationTriggers[animationIndex]);
                animationIndex++;
                currentTime = 0f;
                CookingStateSlider.value = animationIndex;
            }

            // Check if cooking is complete
            if (currentTime >= cookingTime)
            {
                isCooking = false;
                CookingComplete();
            }
        }
    }

    public void StartCooking()
    {
        if (!isCooking)
        {
            isCooking = true;
            currentTime = 0f;
            animationIndex = 0;
        }
    }

    public void StopCooking()
    {
        if (isCooking)
        {
            animationIndex--;
            string trigger = animationTriggers[animationIndex];
            isCooking = false;
            recipeManager.playerActions.Add(trigger);
            string result = recipeManager.CheckSequence();
            Debug.Log(result);
            onCookingComplete.Invoke();
        }
    }

    private void TriggerAnimation(string triggerName)
    {
        // Trigger an animation using the provided trigger name
        if (animator != null)
        {
            animator.SetTrigger(triggerName);
        }
    }

    private void CookingComplete()
    {
        // Cooking is done, invoke the Unity event
        onCookingComplete.Invoke();
    }
}
