using System;
using System.Collections.Generic;

[Serializable]
public class Quest
{
    public string title;
    public string description;
    public List<string> objectives = new List<string>();
    public bool isCompleted;

    public Quest(string title, string description)
    {
        this.title = title;
        this.description = description;
        this.isCompleted = false;
    }

    public bool IsComplete()
    {
        // Check if all objectives are complete
        foreach (string objective in objectives)
        {
            if (!objectiveIsComplete(objective))
            {
                return false;
            }
        }

        // If all objectives are complete, mark the quest as complete
        isCompleted = true;
        return true;
    }

    private bool objectiveIsComplete(string objective)
    {
        // Implement the logic to check if an individual objective is complete.
        // For example, you might compare the player's progress to the objective.
        // Return true if the objective is complete; otherwise, return false.
        return false; // Modify this logic as needed.
    }
}
