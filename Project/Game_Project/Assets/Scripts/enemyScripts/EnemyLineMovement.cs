using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
public class EnemyLineMovement: MonoBehaviour
{
    private Transform player;
    private MovementComponent movementComponent;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        movementComponent = GetComponent<MovementComponent>();
    }

    private void FixedUpdate()
    {
        LineMovement();
    }
    private void LineMovement()
    {
        Vector2 direction =
        (player.position - transform.position).normalized; 

        movementComponent.MoveDirection(direction);
    }
}
