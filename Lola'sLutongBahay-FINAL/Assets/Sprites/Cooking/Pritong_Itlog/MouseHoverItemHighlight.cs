using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverItemHighlight : MonoBehaviour
{
    private Renderer objectRenderer;  // Renderer of the object
    private Material material;  // Material of the object
    private float targetGlow = 0f;  // Target value for GlowSwitch (0 or 1)
    private float currentGlow = 0f;  // Current value of GlowSwitch
    private float glowSpeed = 0.5f;  // Speed of the transition (how fast it goes from 0 to 1)

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();  // Get the Renderer component
        material = objectRenderer.material;  // Get the material from the renderer
    }

    void Update()
    {
        // Gradually change the GlowSwitch value using Lerp
        currentGlow = Mathf.Lerp(currentGlow, targetGlow, Time.deltaTime * glowSpeed);
        material.SetFloat("_GlowSwitch", currentGlow);  // Set the GlowSwitch value in the material
    }

    void OnMouseEnter()
    {
        // Set the targetGlow to 1 when the mouse hovers over the object
        targetGlow = 1f;
    }

    void OnMouseExit()
    {
        // Set the targetGlow to 0 when the mouse leaves the object
        targetGlow = 0f;
    }
}
