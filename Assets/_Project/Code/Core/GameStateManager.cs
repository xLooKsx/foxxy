using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour, IGameStateManager
{
    public GameState CurrentGameState {get; private set; }
 
    public event Action<GameState> OnGameStateChanged;

    public void SetNewGameState(GameState newGameState)
    {
        if(CurrentGameState != newGameState)
        {
            CurrentGameState = newGameState;
            OnGameStateChanged?.Invoke(newGameState);
            print("Novo game state: "+newGameState.ToString());
        }
    }
}
