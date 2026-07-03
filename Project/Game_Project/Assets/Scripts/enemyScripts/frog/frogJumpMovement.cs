using UnityEngine;


class FrogJumpMovement : MonoBehaviour {
    private Transform player;
    private JumpComponent jumpComponent;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        jumpComponent = GetComponent<JumpComponent>();
    }

    private void FixedUpdate()
    {
        JumpMovement();
    }
    private void JumpMovement()
    {
        //Vector2 direction =
        //(player.position - transform.position).normalized;

        //movementComponent.MoveDirection(direction);
        jumpComponent.Jump(player.position);
    }
}