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

    Coroutine countdownCoroutine;

    void Start()
    {
        Player.instance.controls.OnPausedButtonPressed += () => SetState(GameStateType.PauseMenu);
        SetState(GameStateType.MainMenu);
    }

    public void SetState(GameStateType newState)
    {
        if (State == newState) return;
        if (State == GameStateType.MainMenu && newState == GameStateType.PauseMenu) return;

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
        Statistics.instance.ResetTimer();

        XRManager.instance.EnableXRInteractors(true);
        GunManager.instance.EnableGuns(false);
        ForceField.instance.EnableForceField(false);

        UIManager.instance.ShowMainMenu(true);
        UIManager.instance.ShowPauseMenu(false);
        UIManager.instance.ShowGameOverMenu(false);
    }

    void TogglePauseMenu()
    {
        Statistics.instance.ToggleTimer(false);
        StopCountdown();

        XRManager.instance.EnableXRInteractors(true);
        GunManager.instance.EnableGuns(false);
        ForceField.instance.EnableForceField(false);

        UIManager.instance.ShowPauseMenu(true);
        UIManager.instance.ShowMainMenu(false);
        UIManager.instance.ShowGameOverMenu(false);
        UIManager.instance.ShowHolograms(false);
    }

    void ToggleGameOver()
    {
        Statistics.instance.ToggleTimer(false);
        StopCountdown();

        XRManager.instance.EnableXRInteractors(true);
        GunManager.instance.EnableGuns(false);
        ForceField.instance.EnableForceField(false);

        UIManager.instance.ShowHolograms(false);

        UIManager.instance.ShowGameOverMenu(true);
        UIManager.instance.ShowMainMenu(false);
        UIManager.instance.ShowPauseMenu(false);
    }

    void TogglePreInGame()
    {
        StopCountdown();

        XRManager.instance.EnableXRInteractors(false);
        GunManager.instance.EnableGuns(true);
        ForceField.instance.EnableForceField(true);

        UIManager.instance.ShowMainMenu(false);
        UIManager.instance.ShowPauseMenu(false);
        UIManager.instance.ShowGameOverMenu(false);

        countdownCoroutine = StartCoroutine(UIManager.instance.StartCountdown(5f, () => {
            SetState(GameStateType.InGame);
            Statistics.instance.ToggleTimer(true);
            UIManager.instance.ShowHolograms(true);
        }));
    }

    void StopCountdown()
    {
        if (countdownCoroutine != null)
        {
            UIManager.instance.StopCountdown();
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
    }

    /// <summary>
    /// This method checks if the player is currently in game.
    /// </summary>
    /// <returns>True if the current game state is equals to
    /// <see cref="GameStateType.InGame"/>;
    /// otherwise, false.
    /// </returns>
    public bool IsInGame
    {
        get { return State == GameStateType.InGame; }
    }

    /// <summary>
    /// This method checks if the player is currently pre in game.
    /// </summary>
    /// <returns>True if the current game state is equals to
    /// <see cref="GameStateType.PreInGame"/>;
    /// otherwise, false.
    /// </returns>
    public bool IsPreInGame
    {
        get { return State == GameStateType.InGame; }
    }

    /// <summary>
    /// This method returns the current game state.
    /// </summary>
    /// <returns>The current game state.</returns>
    public GameStateType State { get; protected set; }
}
