using UnityEngine;
using UnityEngine.UI;
public class InputManager : MonoBehaviour
{
    [SerializeField] Image blockImage;
    [SerializeField] GameObject player;
    [SerializeField] PlaceableBlockList placeableBlockList;
    void Start()
    {
        InputSystem_Actions input = new InputSystem_Actions();
        PlayerMover playerMover = player.GetComponent<PlayerMover>();
        new PlayerMovementController(input.Player.Move, input.Player.Jump, playerMover);
        new PlayerBlockPlacerController(input.Player.PlaceBlock, input.Player.SwtichBlock, player.GetComponent<BlockPlacer>(), placeableBlockList, blockImage);
    }
}