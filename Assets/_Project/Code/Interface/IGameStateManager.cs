
using System;

public enum GameState
{
    GamePlay,
    Pause,
    Fade,
    Cutscene
}

public interface IGameStateManager
{
    GameState CurrentGameState{get;}

    void SetNewGameState(GameState newGameState);

    event Action<GameState> OnGameStateChanged;
}
