using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController
{
    Canvas PauseMenu;
    private InputAction openCloseAction;
    public MenuController(InputAction openCloseAction, Canvas PauseMenu)
    {
        this.openCloseAction = openCloseAction;
        this.PauseMenu = PauseMenu;
        openCloseAction.performed += ToggleMenu;
        openCloseAction.Enable();
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        ToggleMenu();
    }

    public void ToggleMenu()
    {
        if (GameState._state == GAME_STATE.PAUSED)
        {
            GameState.ChangeState(GAME_STATE.RUNNING);
        }
        else if (GameState._state == GAME_STATE.RUNNING)
        {
            GameState.ChangeState(GAME_STATE.PAUSED);
        }
    }

    public void OnDestroy()
    {
        openCloseAction.performed -= ToggleMenu;
    }
}
