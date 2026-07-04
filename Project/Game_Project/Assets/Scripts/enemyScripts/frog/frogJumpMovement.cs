using System.Collections;
using UnityEngine;

class FrogJumpMovement : MonoBehaviour {
    private Transform player;
    private JumpComponent jumpComponent;
    private bool shouldMove = true;
    public float delayAfterJump = 0.3f;
    
    
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
        if (!shouldMove) return;
        //Vector2 direction =
        //(player.position - transform.position).normalized;

        //movementComponent.MoveDirection(direction);
        jumpComponent.Jump(player.position);
        StartCoroutine(DelayAfterJump());

    }
    private IEnumerator DelayAfterJump() {
        this.shouldMove = false;
        yield return new WaitForSeconds(delayAfterJump);
        this.shouldMove = true;
    }

}