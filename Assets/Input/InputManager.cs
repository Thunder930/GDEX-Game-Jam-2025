using UnityEngine;
public class InputManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Start()
    {
        InputSystem_Actions input = new InputSystem_Actions();
        PlayerMover playerMover = player.GetComponent<PlayerMover>();
        new PlayerMovementController(input.Player.Move, input.Player.Jump, playerMover);
    }
}