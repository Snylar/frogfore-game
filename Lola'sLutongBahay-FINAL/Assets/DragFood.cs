using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFood : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isDragging = false;
    private bool isReleased = false;

    public Collider2D targetCollider;
    public Animator AddingAnimation;
    public PlatingManager platingManager;
    public string actionName;
    private float smoothTime = 11.22f;
    float r;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        isReleased = false;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);

            transform.position = newPosition;
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isReleased = true;
            isDragging = false;
        }
        if (targetCollider != null)
        {
            // Check if the dropped position is inside the collider
            if (targetCollider.bounds.Contains(transform.position))
            {
                Debug.Log("Inside");
                Destroy(gameObject);
                
                if (platingManager != null)
                {
                    platingManager.playerActions.Add(actionName);
                    string result = platingManager.CheckRecipe();
                    if (AddingAnimation != null)
                    {
                        AddingAnimation.Play("AddIngredient");
                    }
                    Debug.Log(result);
                }
                else
                {
                    Debug.Log("None");
                }
            }
        }
    }

    private void Update()
    {
        if (isReleased)
        {
            // Smoothly move the sprite back to its original position
            transform.position = Vector3.Lerp(transform.position, originalPosition, smoothTime * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDragging)
        {
            Debug.Log("In");
            targetCollider = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isDragging && other == targetCollider)
        {
            Debug.Log("Out");
            targetCollider = null;
        }
    }
}


