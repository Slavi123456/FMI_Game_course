using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[RequireComponent(typeof(StatsComponent))]
[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    private StatsComponent stats;
    private Rigidbody2D objRigidbody;
    public void Awake()
    {
        stats = gameObject.GetComponent<StatsComponent>();
        objRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    public void MoveTo(Vector2 target) {
        objRigidbody.MovePosition(target);
    }

    public void MoveDirection(Vector2 direction)
    {
        objRigidbody.linearVelocity = direction * stats.moveSpeed;
    }
    public void stopMovement()
    {
        objRigidbody.linearVelocity = Vector2.zero;
    }
}
