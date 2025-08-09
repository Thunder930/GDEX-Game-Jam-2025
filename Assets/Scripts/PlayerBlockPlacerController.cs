using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;

public class PlayerBlockPlacerController
{
    private BlockPlacer blockPlacer;
    private PlaceableBlockList blockList;
    private int selectedBlockIndex;
    private Image blockImage;
    private InputAction placeBlockAction;
    private InputAction switchBlockAction;
    private InputAction moveMouseAction;
    private Camera camera;

    public PlayerBlockPlacerController(InputAction placeBlockAction, InputAction switchBlockAction, InputAction moveMouseAction, BlockPlacer blockPlacer, PlaceableBlockList blockList, Image blockImage, Camera camera)
    {
        this.placeBlockAction = placeBlockAction;
        this.switchBlockAction = switchBlockAction;
        this.blockPlacer = blockPlacer;
        this.moveMouseAction = moveMouseAction;
        this.camera = camera;
        placeBlockAction.performed += PlaceBlock;
        placeBlockAction.Enable();
        switchBlockAction.performed += SwitchBlock;
        switchBlockAction.Enable();
        moveMouseAction.performed += MoveMouse;
        moveMouseAction.Enable();

        this.blockList = blockList;
        this.blockImage = blockImage;
        blockImage.sprite = blockList.placeableBlocks[selectedBlockIndex].m_DefaultSprite;
        blockPlacer.currentTile = blockList.placeableBlocks[selectedBlockIndex];
    }

    private void PlaceBlock(InputAction.CallbackContext context)
    {
        if (GameState._state == GAME_STATE.RUNNING)
        {
            if (blockPlacer.PlaceBlock())
            {
                blockPlacer.IncrementCounter(selectedBlockIndex);
            }
        }
    }

    private void SwitchBlock(InputAction.CallbackContext context)
    {
        if (GameState._state == GAME_STATE.RUNNING)
        {
            float input = context.ReadValue<Vector2>().y;
            if (input > 0.0f)
            {
                selectedBlockIndex = (selectedBlockIndex + 1) % blockList.placeableBlocks.Length;
            }
            else if (input < 0.0f)
            {
                if (selectedBlockIndex == 0)
                {
                    selectedBlockIndex = blockList.placeableBlocks.Length - 1;
                }
                else
                {
                    selectedBlockIndex--;
                }
            }
            blockImage.sprite = blockList.placeableBlocks[selectedBlockIndex].m_DefaultSprite;
            blockPlacer.currentTile = blockList.placeableBlocks[selectedBlockIndex];
        }
    }

    private void MoveMouse(InputAction.CallbackContext context)
    {
        if (GameState._state == GAME_STATE.RUNNING)
        {
            Vector2 location = camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
            blockPlacer.Move(location);
        }
    }

    public void OnDestroy()
    {
        placeBlockAction.performed -= PlaceBlock;
        switchBlockAction.performed -= SwitchBlock;
        moveMouseAction.performed -= MoveMouse;
    }
}