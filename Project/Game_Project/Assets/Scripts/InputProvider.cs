using UnityEngine;
using UnityEngine.InputSystem;

public class InputProvider : MonoBehaviour
{
    public InputActionReference playerAbilityButton;
    public InputActionReference playerMovement;

    public bool isAbilityActivated { get; private set; }
    public Vector2 moveDirection { get; private set; }

    private void Start() {
        isAbilityActivated = false;
    }
    public void Update()
    {
        if (playerAbilityButton == null || playerMovement == null) { return; } //or Exit the game
        isAbilityActivated = playerAbilityButton.action.WasPressedThisFrame();
        moveDirection = playerMovement.action.ReadValue<Vector2>();
    }

    //A way to refactor later on with using PlayerInput component
    //public void OnMove(InputAction.CallbackContext ctx)
    //{
    //    moveDirection = ctx.ReadValue<Vector2>();
    //}
}
