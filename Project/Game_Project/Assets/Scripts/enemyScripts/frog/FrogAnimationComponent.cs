using UnityEngine;


class FrogAnimationComponent : MonoBehaviour { 
    private SpriteRenderer spriteRenderer;
    private JumpComponent jumpComponent;
    private Animator animator;

    public Sprite fallSprite;
    public Sprite jumpSprite;
    public AnimationClip idleAnim;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpComponent = GetComponent<JumpComponent>();  
        animator = GetComponent<Animator>();
    }

    public void UpdateMovement()
    {
        switch (jumpComponent.jumpState) {
            case JumpComponent.JumpState.TakeOff:
                spriteRenderer.sprite = jumpSprite;
                break;
            case JumpComponent.JumpState.Jump:
                
                break;
                break;
            case JumpComponent.JumpState.Land:
                spriteRenderer.sprite = fallSprite;
                //make through a sin curve a spirte up offset
                break;
            case JumpComponent.JumpState.Idle:
                //play anim clip
                break;
            default:
                break;
        }
    }

}