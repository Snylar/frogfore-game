using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlatingManager : MonoBehaviour
{
    public List<string> recipe = new List<string>(); // List of required actions in the recipe
    public List<string> playerActions = new List<string>(); // List to store the player's actions

    public UnityEvent correctEvent;
    public UnityEvent failedEvent;

    // Call this function when the Serve button is pressed
    public void OnServeButtonPressed()
    {
        string result = CheckRecipe();
        Debug.Log(result);
    }

    // Checks the player's actions against the recipe
    public string CheckRecipe()
    {
        if (ContainsAllRecipeActions())
        {
            correctEvent.Invoke();
            return "Congratulations! You've completed the recipe.";
        }
        else
        {
            failedEvent.Invoke();
            return "Oops! You missed some steps in the recipe.";
        }
    }

    // Check if all actions in the recipe are found in playerActions (order doesn't matter)
    private bool ContainsAllRecipeActions()
    {
        List<string> PlayerActions = new List<string>(playerActions);

        foreach (string action in recipe)
        {
            if (PlayerActions.Contains(action))
            {
                PlayerActions.Remove(action); // Remove to handle duplicates correctly
            }
            else
            {
                return false; // Missing an action
            }
        }
        return true;
    }
}
