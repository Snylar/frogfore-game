using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float speed = 10f;

    [SerializeField] Rigidbody2D playerRigidBody;
    [SerializeField] Animator playerAnimator;

    public string transitionName;

    private Vector3 bottomLeftEdge;
    private Vector3 topRightEdge;
    public FixedJoystick joystick;

    public bool deactivateMovement = false;

    [SerializeField] Tilemap tilemap;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        bottomLeftEdge = tilemap.localBounds.min + new Vector3(-2.5f, -2.5f, 0f);
        topRightEdge = tilemap.localBounds.max + new Vector3(5f, 1.2f, 0f);
    }

    void Update()
    {
        if (deactivateMovement || (InputManager.Instance != null && InputManager.Instance.IsMenuActive()))
        {
            playerRigidBody.velocity = Vector2.zero;
            playerAnimator.SetFloat("movementX", 0);
            playerAnimator.SetFloat("movementY", 0);
            return; // Skip movement if menu is active or movement is deactivated
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        playerRigidBody.velocity = new Vector2(horizontal, vertical).normalized * speed;

        playerAnimator.SetFloat("movementX", playerRigidBody.velocity.x);
        playerAnimator.SetFloat("movementY", playerRigidBody.velocity.y);

        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            playerAnimator.SetFloat("lastX", horizontal);
            playerAnimator.SetFloat("lastY", vertical);
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftEdge.x, topRightEdge.x),
            Mathf.Clamp(transform.position.y, bottomLeftEdge.y, topRightEdge.y),
            Mathf.Clamp(transform.position.z, bottomLeftEdge.z, topRightEdge.z));
    }

    public void DeactivateMovement()
    {
        deactivateMovement = true;
    }

    public void DestroyMePls()
    {
        Destroy(gameObject);
    }
}