
using System;

public enum GameState
{
    GamePlay,
    Pause,
    Cutscene
}

public interface IGameStateManager
{
    GameState CurrentGameState{get;}

    void SetNewGameState(GameState newGameState);

    event Action<GameState> OnGameStateChanged;
}
