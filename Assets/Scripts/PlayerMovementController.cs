using UnityEngine.InputSystem;

public class PlayerMovementController
{
    private PlayerMover playerMover;
    public PlayerMovementController(InputAction moveAction, InputAction jumpAction, PlayerMover playerMover)
    {
        this.playerMover = playerMover;
        moveAction.performed += StartMovement;
        jumpAction.performed += Jump;
        moveAction.canceled += StopMovement;
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void StartMovement(InputAction.CallbackContext context)
    {
        playerMover.Move(context.ReadValue<float>());
    }

    private void StopMovement(InputAction.CallbackContext context)
    {
        playerMover.StopMovement();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        playerMover.Jump();
    }
}