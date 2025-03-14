using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D defaultCursor;  // Cursor sprite for normal state
    public Texture2D clickCursor;    // Cursor sprite for clicking
    public Vector2 hotSpot = Vector2.zero; // Adjust click point
    private bool isCursorVisible = false;

    void Start()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
        Cursor.visible = false; // Start with cursor hidden
    }

    void Update()
    {
        // Toggle cursor visibility with Alt key
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            isCursorVisible = !isCursorVisible;
            Cursor.visible = isCursorVisible;
        }

        // Change cursor sprite based on mouse click
        if (isCursorVisible)
        {
            if (Input.GetMouseButton(0)) // Left mouse button down
            {
                Cursor.SetCursor(clickCursor, hotSpot, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
            }
        }
    }
}