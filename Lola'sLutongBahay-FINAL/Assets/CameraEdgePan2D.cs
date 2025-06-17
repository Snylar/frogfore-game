using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgePan2D : MonoBehaviour
{
    public float panSpeed = 10f;             // Speed at which the camera pans
    public float edgeThickness = 10f;        // Thickness (in pixels) of the screen edge
    public Vector2 minCameraPos;             // Minimum X and Y positions for the camera
    public Vector2 maxCameraPos;             // Maximum X and Y positions for the camera

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x >= Screen.width - edgeThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        else if (mousePos.x <= edgeThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        if (mousePos.y >= Screen.height - edgeThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        else if (mousePos.y <= edgeThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }

        // Clamp camera position to stay within bounds
        pos.x = Mathf.Clamp(pos.x, minCameraPos.x, maxCameraPos.x);
        pos.y = Mathf.Clamp(pos.y, minCameraPos.y, maxCameraPos.y);

        transform.position = pos;
    }
}

