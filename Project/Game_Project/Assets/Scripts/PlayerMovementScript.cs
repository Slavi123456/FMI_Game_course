using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public InputActionReference movement;
    public Vector2 movementSpeed;
    private Vector2 direction;
    private Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    public void handleMovement() {
        direction = movement.action.ReadValue<Vector2>();
        rigidbody.linearVelocity = direction * movementSpeed;
        //Debug.Log(direction);
    }
}
