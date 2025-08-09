using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InputManager : MonoBehaviour
{
    [SerializeField] Image blockImage;
    [SerializeField] GameObject cursor;
    [SerializeField] PlaceableBlockList placeableBlockList;
    [SerializeField] Canvas PauseMenu;
    [SerializeField] Camera cam;
    private MenuController menuController;
    private InputSystem_Actions input;
    private PlayerBlockPlacerController playerBlockPlacerController;
    void Start()
    {
        input = new InputSystem_Actions();
        playerBlockPlacerController = new PlayerBlockPlacerController(input.Player.PlaceBlock, input.Player.SwtichBlock, input.UI.Point, cursor.GetComponent<BlockPlacer>(), placeableBlockList, blockImage, cam);
        menuController = new MenuController(input.UI.Pause, PauseMenu);
    }

    public void CloseMenu()
    {
        menuController.ToggleMenu();
    }

    public void ReturnToMainMenu()
    {
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
        playerBlockPlacerController.OnDestroy();
        input.Disable();
        input.Dispose();
    }
}