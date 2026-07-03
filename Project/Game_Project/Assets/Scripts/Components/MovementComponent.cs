using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[RequireComponent(typeof(StatsComponent))]
[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    private StatsComponent stats;
    private Rigidbody2D objRigidbody;

    public bool HasReachDestination { get; private set; }
    private void Awake()
    {
        stats = gameObject.GetComponent<StatsComponent>();
        objRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        HasReachDestination = true;
    }
    public void MoveTo(Vector2 target) {
        this.HasReachDestination = false;
        objRigidbody.MovePosition(target);
        this.HasReachDestination = true;   
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
