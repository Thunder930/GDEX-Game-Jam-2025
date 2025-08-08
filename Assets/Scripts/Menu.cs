using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GAME_STATE activeState;
    [SerializeField] GameObject menu;

    private void Start()
    {
        GameState.OnGameStateChange += DetermineMenuActive;
        menu.SetActive(false);
    }

    private void DetermineMenuActive(GAME_STATE state)
    {
        if (GameState._state == activeState)
        {
            menu.SetActive(true);
        }
        else
        {
            menu.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        GameState.OnGameStateChange -= DetermineMenuActive;
    }
}