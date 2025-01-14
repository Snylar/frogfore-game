using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float speed = 10f; // movement speed

    [SerializeField] Rigidbody2D playerRigidBody; // reference to rigidbody component
    [SerializeField] Animator playerAnimator;

    public string transitionName;

    private Vector3 bottomLeftEdge;
    private Vector3 topRightEdge;
    public FixedJoystick joystick;

    public bool deactivateMovement = false;

    [SerializeField] Tilemap tilemap;

    void Awake()
    {
        
    }
    void Start()
    {

        DontDestroyOnLoad(gameObject);

        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        bottomLeftEdge = tilemap.localBounds.min + new Vector3(-2.5f, -2.5f, 0f);
        topRightEdge = tilemap.localBounds.max + new Vector3(5f, 1.2f, 0f);
    }

    void Update()
    {
        // read input axes
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        if (deactivateMovement)
        {
            playerRigidBody.velocity = Vector2.zero;
        }
        else
        {
            // apply movement to rigidbody
            playerRigidBody.velocity = new Vector2(horizontal, vertical).normalized * speed;
        }


        playerAnimator.SetFloat("movementX", playerRigidBody.velocity.x);
        playerAnimator.SetFloat("movementY", playerRigidBody.velocity.y);

        if(horizontal >= 0.1 || horizontal <= -0.1 || vertical >= 0.1 || vertical <= -0.1)
        {
            Debug.Log("The condition is true!");
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
        Destroy(this.gameObject);
    }
}
