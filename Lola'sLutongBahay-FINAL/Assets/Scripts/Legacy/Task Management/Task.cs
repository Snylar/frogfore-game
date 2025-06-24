using UnityEngine;

[System.Serializable]
public class Task
{
    public string title;
    public string subtitle;
    public bool taskComplete;

    public Task(string title)
    {
        this.title = title;
        this.taskComplete = false;
    }

    public void MarkAsComplete()
    {
        taskComplete = true;
    }
}
