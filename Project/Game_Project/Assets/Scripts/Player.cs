using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public enum PlayerState { 
        Normal,
        UsingAbility
    }
    public InputActionReference button;
    
    private PlayerMovementScript move;
    private PlayerShadowSkillScript skill;
    private PlayerState state;

    void Start()
    {
        move = GetComponent<PlayerMovementScript>();
        skill = GetComponent<PlayerShadowSkillScript>();
    }
    // Update is called once per frame
    void Update()
    {
        if (button == null)
        {
            return;
        }
        if (button.action.WasPressedThisFrame() && state == PlayerState.Normal)
        {
            state = PlayerState.UsingAbility;
            skill.handleShadowClone(this);
            return;
        }
        else if (state == PlayerState.Normal) 
        { 
            move.handleMovement();
        }
    }

    public void SetState(PlayerState newState) {
        state = newState;
    }
}
