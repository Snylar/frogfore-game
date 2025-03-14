using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private bool isMenuActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    public bool IsMenuActive()
    {
        return isMenuActive;
    }

    public void SetMenuActive(bool active)
    {
        isMenuActive = active;
        Debug.Log("Menu Active: " + active);
    }
}