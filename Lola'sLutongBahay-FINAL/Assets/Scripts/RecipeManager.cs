using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RecipeManager : MonoBehaviour
{
    public List<string> recipe = new List<string>(); // List of required actions in the recipe
    public List<string> playerActions = new List<string>(); // List to store the player's actions

    public UnityEvent correctEvent;
    public UnityEvent failedEvent;


    private int ActionCount;
    // Function to check if the player's actions match the recipe
    public string CheckSequence()
    {
        if (playerActions.Count < recipe.Count)
        {
            return "Keep going, you still need to perform more actions.";
        }
        else if (ListsAreEqual(playerActions, recipe))
        {
            correctEvent.Invoke();
            return "Congratulations! You've completed the recipe.";
        }
        else if (ListsAreEqual(playerActions.GetRange(0, recipe.Count), recipe))
        {
            failedEvent.Invoke();
            Debug.Log("Hello");
            return "You've performed some of the correct actions, but there are more to do in the recipe.";
        }
        else
        {
            failedEvent.Invoke();
            return "Oops! Your actions are not in the correct sequence. Try again.";
        }
    }

    // Helper function to check if two lists contain the same elements in the same order
    private bool ListsAreEqual(List<string> list1, List<string> list2)
    {
        if (list1.Count != list2.Count)
            return false;

        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
                return false;
        }

        return true;
    }

    private void Update()
    {

    }
}

