using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

class PlayerAnimationComponent : MonoBehaviour {

    private Animator anim; 
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    public void UpdateMovementAnim(Vector2 movement) {
        anim.SetFloat("XcoordDir", movement.x);
        anim.SetFloat("YcoordDir", movement.y);

        if (movement.x != 0.0f) { 
            spriteRenderer.flipX = movement.x < 0.0f;
        }
    }
}