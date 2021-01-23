using UnityEngine;

public enum GameStateType
{
    Initial,
    MainMenu,
    PauseMenu,
    GameOver,
    PreInGame,
    InGame,
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

    public delegate void GameStateChanged(GameStateType state);
    public event GameStateChanged OnGameStateChanged;

    void Start()
    {
        Player.instance.controls.OnPausedButtonPressed += () => SetState(GameStateType.PauseMenu);
        SetState(GameStateType.MainMenu);
    }

    public void SetState(GameStateType newState)
    {
        if (State == newState) return;
        State = newState;

        switch (newState)
        {
            case GameStateType.MainMenu:
                ToggleMainMenu();
                break;

            case GameStateType.PauseMenu:
                TogglePauseMenu();
                break;

            case GameStateType.GameOver:
                ToggleGameOver();
                break;

            case GameStateType.PreInGame:
                TogglePreInGame();
                break;
        }

        OnGameStateChanged?.Invoke(State);
    }

    void ToggleMainMenu()
    {
        XRManager.instance.EnableXRInteractors(true);
        GunManager.instance.EnableGuns(false);

        UIManager.instance.ShowMainMenu(true);
        UIManager.instance.ShowPauseMenu(false);
        UIManager.instance.ShowHologram(false);
    }

    void TogglePauseMenu()
    {
        XRManager.instance.EnableXRInteractors(true);
        GunManager.instance.EnableGuns(false);

        UIManager.instance.ShowPauseMenu(true);
        UIManager.instance.ShowMainMenu(false);
        UIManager.instance.ShowHologram(false);
    }

    void ToggleGameOver()
    {
        XRManager.instance.EnableXRInteractors(true);
        GunManager.instance.EnableGuns(false);

        UIManager.instance.ShowMainMenu(false);
        UIManager.instance.ShowPauseMenu(false);
    }

    void TogglePreInGame()
    {
        XRManager.instance.EnableXRInteractors(false);
        GunManager.instance.EnableGuns(true);

        UIManager.instance.ShowHologram(true);
        UIManager.instance.ShowMainMenu(false);
        UIManager.instance.ShowPauseMenu(false);

        StartCoroutine(UIManager.instance.StartCountdown(() => {
            SetState(GameStateType.InGame);
        }));
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
        get { return State == GameStateType.InGame; }
    }

    /// <summary>
    /// This method returns the current game state.
    /// </summary>
    /// <returns>The current game state.</returns>
    public GameStateType State { get; protected set; }

}
