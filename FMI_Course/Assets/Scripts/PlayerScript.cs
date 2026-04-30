using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerScript : MonoBehaviour
{
    private Transform tran;
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private Vector2 movementDir;
    
    private int collectedGems = 0;
    private bool isOnGround = false;

    public Vector2 movementSpeed = new Vector2(10.0f, 0.0f);
    public float jumpImpulse = 10.0f;

    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public Transform feet;
    public LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Player is spawned");
        tran = GetComponent<Transform>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkMoveAction();
    }
    public void collectGem() {
        Debug.Log("Player collected one GEM!!!");
    }

    private void checkMoveAction()
    {
        isOnGround = Physics2D.OverlapBox(feet.position, new Vector2(0.55f, 0.1f), 0f, groundLayer);
        handleMovement();
        handleJumps();
    }
    private void handleMovement() {
        movementDir = moveAction.action.ReadValue<Vector2>();
        rigidbody2D.linearVelocity = new Vector2(movementDir.x * movementSpeed.x, rigidbody2D.linearVelocity.y);
    }
    private void handleJumps() {
        //jumpAction.action.triggered && isOnGround - for one click
        bool jumpPressed = jumpAction.action.ReadValue<float>() > 0.5f;
        if (jumpPressed && isOnGround)
        {
            rigidbody2D.linearVelocity = new Vector2(
                rigidbody2D.linearVelocity.x,
                jumpImpulse
            );
        }
    }
}
