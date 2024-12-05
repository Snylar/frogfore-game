using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragIngredients : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isDragging = false;
    private bool isReleased = false;

    [SerializeField] int rotateangle;
    public Collider2D targetCollider;
    public string actionName;
    public RecipeManager recipeManager;
    private float smoothTime = 11.22f;
    float r;
   
    [SerializeField] Sprite EmptyBowl;
    public UnityEvent actionEvent;

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
                recipeManager.playerActions.Add(actionName);
                string result = recipeManager.CheckSequence();
                Debug.Log(result);
                this.GetComponent<SpriteRenderer>().sprite = EmptyBowl;
                transform.eulerAngles = Vector3.forward * 0;
                actionEvent.Invoke();
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
            transform.eulerAngles = Vector3.forward * rotateangle;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isDragging && other == targetCollider)
        {
            Debug.Log("Out");
            targetCollider = null;
            transform.eulerAngles = Vector3.forward * 0;
        }
    }
}
