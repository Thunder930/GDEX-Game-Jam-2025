using System;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_STATE
{
    RUNNING,
    WON,
    LOST,
    PAUSED
};

public static class GameState
{
    public static GAME_STATE _state { private set; get; }
    public static event Action<GAME_STATE> OnGameStateChange;
    private static List<GAME_STATE> pausedStates = new List<GAME_STATE>() 
    {
        GAME_STATE.WON,
        GAME_STATE.LOST,
        GAME_STATE.PAUSED
    };
    public static void ChangeState(GAME_STATE state)
    {
        if (state != _state)
        {
            if (pausedStates.Contains(state))
            {
                Time.timeScale = 0f;
            } else
            {
                Time.timeScale = 1f;
            }
            _state = state;
            OnGameStateChange?.Invoke(state);
        }
    }
}
