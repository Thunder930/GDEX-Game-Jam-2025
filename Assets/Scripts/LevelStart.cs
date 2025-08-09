using UnityEngine;

public class LevelStart : MonoBehaviour
{
    void Start()
    {
        GameState.ChangeState(GAME_STATE.RUNNING);
    }
}
