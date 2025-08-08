using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InputManager : MonoBehaviour
{
    [SerializeField] Image blockImage;
    [SerializeField] GameObject player;
    [SerializeField] PlaceableBlockList placeableBlockList;
    [SerializeField] Canvas PauseMenu;
    private MenuController menuController;
    private InputSystem_Actions input;
    void Start()
    {
        input = new InputSystem_Actions();
        PlayerMover playerMover = player.GetComponent<PlayerMover>();
        new PlayerMovementController(input.Player.Move, input.Player.Jump, playerMover);
        new PlayerBlockPlacerController(input.Player.PlaceBlock, input.Player.SwtichBlock, player.GetComponent<BlockPlacer>(), placeableBlockList, blockImage);
        menuController = new MenuController(input.UI.Pause, PauseMenu);
    }

    public void CloseMenu()
    {
        menuController.ToggleMenu();
    }

    public void ReturnToMainMenu()
    {
        GameState.ChangeState(GAME_STATE.RUNNING);
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        LevelManager.LoadNextLevel();
    }

    public void ReloadLevel()
    {
        LevelManager.ReloadLevel();
    }

    private void OnDestroy()
    {
        menuController.OnDestroy();
        input.Disable();
        input.Dispose();
    }
}