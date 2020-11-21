using UnityEngine;

public enum GameStateType
{
    GamePaused,
    GameOver,
    InGame,
    TargetAcquisition
}


public class GameState : MonoBehaviour
{
    #region Singleton

    public static GameState instance;

    void Awake()
    {
        instance = this;  
    }

    #endregion

    public GameStateType State { get; protected set; } = GameStateType.InGame;

    public void SetState(GameStateType newState)
    {
        if (State == newState) return;
        State = newState;

        switch (newState)
        {
            case GameStateType.GamePaused:
                // TODO: Toggle game paused menu
                break;

            case GameStateType.GameOver:
                // TODO: Toggle game over screen
                break;

            case GameStateType.InGame:
             
                break;

            case GameStateType.TargetAcquisition:
               
                break;

        }
    }

}
