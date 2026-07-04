using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FrogAnimationComponent : MonoBehaviour
{
    private Animator animator;
    private JumpComponent jump;

    private JumpComponent.JumpState previousState;
    private SpriteRenderer spriteRenderer;
    private static readonly int TakeOff = Animator.StringToHash("TakeOff");
    
    private void Awake ()
    {
        animator = GetComponent<Animator>();
        jump = GetComponentInParent<JumpComponent>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        previousState = jump.jumpState;
    }

    
    void Update()
    {
        if (previousState == jump.jumpState) return;

        previousState = jump.jumpState;
        if (jump.JumpDirection.x > 0)
            spriteRenderer.flipX = true;
        else if (jump.JumpDirection.x < 0)
            spriteRenderer.flipX = false;

        if (jump.jumpState == JumpComponent.JumpState.TakeOff)
        {
            animator.SetTrigger(TakeOff);
        }
    }
}
