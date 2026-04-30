using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public InputActionReference movement;
    public Vector2 movementSpeed;
    private Vector2 direction;
    private Rigidbody2D rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
    }

    void handleMovement() {
        direction = movement.action.ReadValue<Vector2>();
        rigidbody.linearVelocity = direction * movementSpeed;
        //Debug.Log(direction);
    
    }
}
