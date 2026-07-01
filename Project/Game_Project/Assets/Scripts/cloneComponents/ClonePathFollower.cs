using System.IO;
using UnityEngine;


[RequireComponent(typeof(MovementComponent))]
[RequireComponent(typeof(ClonePathRecorderComp))]
class ClonePathFollower: MonoBehaviour { 
    
    MovementComponent movementComponent;
    ClonePathRecorderComp clonePathRecorderComp;

    private void Awake()
    {
        movementComponent = GetComponent<MovementComponent>();
        clonePathRecorderComp = GetComponent<ClonePathRecorderComp>();
    }
    public void Follow() {
        if (clonePathRecorderComp.HasPoints) {
        
            Vector2 target = clonePathRecorderComp.CurrentPoint();
            Vector2 direction = (target - (Vector2)gameObject.transform.position).normalized;

            movementComponent.MoveDirection(direction);


            if (Vector2.Distance(gameObject.transform.position, target) < 0.1f) { 
                clonePathRecorderComp.RemoveCurrentPoint();
            }
        }
        
    }
}