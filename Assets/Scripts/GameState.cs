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
                TogglePauseMenu();
                break;

            case GameStateType.GameOver:
                ToggleGameOver();
                break;

            case GameStateType.InGame:
                ToggleIngame();
                break;

            case GameStateType.TargetAcquisition:
                ToggleTargetAcquisition();
                break;

        }
    }

    /// <summary>
    /// Toggles the game state <see cref="GameStateType.GamePaused"/> and hides
    /// all unrelevant information.
    /// </summary>
    void TogglePauseMenu()
    {
        // TODO: Toggle game paused menu
    }

    /// <summary>
    /// Toggles the game state <see cref="GameStateType.GameOver"/> and hides
    /// all unrelevant information.
    /// </summary>
    void ToggleGameOver()
    {
        // TODO: Toggle game over screen
    }

    /// <summary>
    /// Disables the game state <see cref="GameStateType.InGame"/> and displays
    /// the game relevant informations.
    /// </summary>
    void ToggleIngame()
    {
    }

    void ToggleTargetAcquisition()
    {
    }




    /// <summary>
    /// This method checks if the player is currently in game.
    /// </summary>
    /// <returns>True if the current game state is equals to
    /// <see cref="GameStateType.InGame"/> and <see cref="GameStateType.TargetAcquisition"/>;
    /// otherwise, false.
    /// </returns>
    public bool IsInGame
    {
        get { return State == GameStateType.InGame || State == GameStateType.TargetAcquisition; }
    }

    /// <summary>
    /// This method checks if the player is currently in target acquisition mode.
    /// </summary>
    /// <returns>True if the current game state is equals to
    /// <see cref="GameStateType.TargetAcquisition"/>; otherwise, false.
    /// </returns>
    public bool IsInTargetAcquisition
    {
        get { return State == GameStateType.TargetAcquisition; }
    }


}
