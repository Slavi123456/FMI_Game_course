using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
class JumpComponent: MonoBehaviour {
    public enum JumpState {
        Idle,
        TakeOff,
        Jump,
        Land,
    }
    
    private MovementComponent move;
    public JumpState jumpState { get; private set; }
    public float Progress { get; private set; }
    public bool IsJumping => jumpState != JumpState.Idle;
    public Vector2 JumpDirection { get; private set; }

    [Header("Jump")]
    public float jumpTakeOffTime = 0.1f;
    public float jumpDuration = 0.6f;
    public float jumpLandingTime = 0.1f;

    private void Awake() {
        this.move = GetComponent<MovementComponent>();
        JumpDirection = new Vector2();
    }

    public void Jump(Vector2 destination) {
        if (IsJumping) return;
        Vector2 start = transform.position;
        JumpDirection = (destination - start).normalized;
        StartCoroutine(Jumping(destination));
    }
    private IEnumerator Jumping(Vector2 destination)
    {
        this.Progress = 0;
     
        this.jumpState = JumpState.TakeOff;
        yield return new WaitForSeconds(this.jumpTakeOffTime);
        
        this.jumpState = JumpState.Jump;
        //this.move.MoveDirection(direction);
        float timer = 0.0f;
        Vector2 startPosition = (Vector2)transform.position;
        while (timer < jumpDuration) { 
            timer += Time.deltaTime;
            Progress = Mathf.Clamp01(timer / jumpDuration);

            
            move.MoveTo(Vector2.Lerp(startPosition, destination, Progress));

            yield return null;
        }
        //yield return new WaitUntil(() => move.HasReachDestination);
        this.jumpState = JumpState.Land;
        this.Progress = 1;

        yield return new WaitForSeconds(this.jumpLandingTime);
        this.jumpState = JumpState.Idle;
        this.Progress = 0;

    }
}