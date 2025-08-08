using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelSetup : MonoBehaviour
{
    void Start()
    {
        LevelManager.SetTileMap(GetComponent<Tilemap>());
        GameState.OnGameStateChange += ChangeGameState;
    }

    private void Update()
    {
        LevelManager.Update();
    }

    private void ChangeGameState(GAME_STATE state)
    {
        Debug.Log("Game State changed to " + state);
    }

    private void OnDestroy()
    {
        GameState.OnGameStateChange -= ChangeGameState;
    }
}