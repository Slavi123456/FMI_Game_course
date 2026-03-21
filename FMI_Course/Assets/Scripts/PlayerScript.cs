using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerScript : MonoBehaviour
{
    ///////////////////////////////////////////
    ///Manually checking if the button is pressed
    private Transform tran;
    private Rigidbody2D rigidbody2D;

    private bool haveDoubleJumped = false;
    private bool isOnGround = true;

    [Header("Player Movement Settings")]
    [Tooltip("Do you want your movement physical and smooth and to be cumilative with friction.")]
    public bool isRigidbodyMovement = false;
    [Header("Jump Settings")]
    [Tooltip("Do you want your jump to have forward movement")]
    public bool isForwardJump = false;

    public Vector2 movementSpeed = new Vector2(10.0f, 0.0f);
    public Vector2 forwardJumpImpulse = new Vector2(1.0f, 10.0f);
    public float jumpImpulse = 10.0f;

    ///////////////////////
    ///For the smarter way
    private Vector2 movementDir;
    private float haveJumped;
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Player is spawned");
        tran = GetComponent<Transform>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkMoveAction();
    }

    void checkMoveAction()
    {
        //These work even when it is not play mode
        //InputSystem.onEvent += 
        //    (eventPtr, device) =>
        //{
        //    Debug.Log("Device" + device + " " + eventPtr);
        //};
        //InputSystem.onEvent.ForDevice(Keyboard.current).Call(e => Debug.Log("KeyBoard event"));

        ///////////////////////////////////////////
        ///Manually checking if the button is pressed
        //if (Keyboard.current.aKey.IsPressed()) {
        //    if (isRigidbodyMovement)
        //    {
        //        //Rigidbody movement works over time through the physics engine and doesn't need Time.deltaTime
        //        //
        //        //Directly assings movement and it's not influenced through mass. Good for constant movement
        //        rigidbody2D.linearVelocity = new Vector2(-movementSpeed.x, rigidbody2D.linearVelocity.y);

        //        //Gives Instantaneous Push and it's cumilative movement
        //        //rigidbody2D.AddForceX(movementSpeed.x * -100.0f);
        //    }
        //    else { 
        //        transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        //    }
        //}
        //if (Keyboard.current.dKey.IsPressed()) {
        //    if (isRigidbodyMovement)
        //    {
        //        //Rigidbody movement works over time through the physics engine and doesn't need Time.deltaTime
        //        //
        //        //Directly assings movement and it's not influenced through mass. Good for constant movement
        //        rigidbody2D.linearVelocity = new Vector2(movementSpeed.x, rigidbody2D.linearVelocity.y);

        //        //Gives Instantaneous Push and it's cumilative movement
        //        //rigidbody2D.AddForceX(movementSpeed.x * -100.0f);
        //    }
        //    else
        //    {
        //        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        //    }
        //}
        //if (Keyboard.current.wKey.IsPressed() || Keyboard.current.spaceKey.IsPressed()) {
        //    if (isForwardJump)
        //    {
        //        rigidbody2D.AddForce(forwardJumpImpulse);
        //    }
        //    else {
        //        rigidbody2D.AddForceY(jumpImpulse);
        //    }
        //}

        /////////////////////////////////////////////////////////////
        ///Smarter way to have the action 
        ///
        movementDir = moveAction.action.ReadValue<Vector2>();
        haveJumped = jumpAction.action.ReadValue<float>();

        //isOnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        rigidbody2D.linearVelocity = new Vector2(movementDir.x * movementSpeed.x, rigidbody2D.linearVelocity.y + haveJumped * jumpImpulse);
    }
}
