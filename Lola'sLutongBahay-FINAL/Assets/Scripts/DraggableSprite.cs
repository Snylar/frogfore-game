using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableSprite : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isDragging = false;
    private bool isReleased = false;

    public Collider2D targetCollider;
    public ParticleSystem saltparticleSystem;
    private float smoothTime = 11.22f;
    float r;

    private void Start()
    {
        saltparticleSystem.Stop();
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
                // Play the particle system
                if (saltparticleSystem != null)
                {
                    saltparticleSystem.Play();
                    transform.eulerAngles = Vector3.forward * 0;
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
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, 250 , ref r, 0.1f);
            transform.eulerAngles = Vector3.forward * angle;
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

