using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCooking : MonoBehaviour
{
    public bool isCooking = false; // Is the egg cooking?
    public bool isSalted = false;
    public bool isPeppered = false;
    public Sprite underdoneSprite; // Sprite for underdone state
    public Sprite mediumRareSprite; // Sprite for medium rare state
    public Sprite doneSprite; // Sprite for done state
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component
    private float cookingTimer = 0f; // Timer for cooking
    public int cookingState = 0; // Cooking state: 0 = undercooked, 1 = cooked, 2 = overcooked
    public Vector2 eggPosition;
    
    private void Start()
    {
        // Get the reference to the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite to undercooked
        spriteRenderer.sprite = underdoneSprite;

    }

    private void Update()
    {
        if(isCooking)
        {
             cookingTimer += Time.deltaTime;

            // Check if the cooking timer has reached the threshold for changing states
            if (cookingTimer >= 10f)
            {
                cookingTimer = 0f; // Reset the timer

                // Change the cooking state
                cookingState++;

                // Wrap the cooking state if it goes beyond the maximum value
                if (cookingState > 2)
                    cookingState = 0;
                // Display the current cooking state
                switch (cookingState)
                {
                    case 0:
                        spriteRenderer.sprite = underdoneSprite;
                        break;
                    case 1:
                        spriteRenderer.sprite = mediumRareSprite;
                        break;
                    case 2:
                        spriteRenderer.sprite = doneSprite;
                        isCooking = false; // Stop the timer when it reaches overcooked state
                        break;
                }
            }
        }
    }
    private void OnMouseDown()//Evaluation of the Dish
    {
        CookingManager gameManager = CookingManager.Instance;
        Debug.Log("Yes");
        transform.position = new Vector3(eggPosition.x, eggPosition.y, 0f);
        if(cookingState == 1)
        {
            gameManager.Victory();
            Debug.Log("Do");
        }
        else
        {
            gameManager.GameOver();
        }
    }
}
