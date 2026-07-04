using UnityEngine;


class FrogVisualComponent : MonoBehaviour {
    private JumpComponent jumpComponent;    
    private Vector3 initialLocalPosition;
    [SerializeField]
    private AnimationCurve jumpCurve;
    [SerializeField]
    private float jumpHeight = 1.2f;
    [SerializeField]
    private Transform spriteRoot;


    private void Awake()
    { 
        jumpComponent = GetComponentInParent<JumpComponent>();
        initialLocalPosition = spriteRoot.localPosition;
    }

    public void Update()
    {
        if (!jumpComponent.IsJumping) {
            spriteRoot.localPosition = initialLocalPosition;
            return;
        }

        float height = jumpCurve.Evaluate(jumpComponent.Progress);
        spriteRoot.localPosition = initialLocalPosition + Vector3.up * height * jumpHeight;
    }

}