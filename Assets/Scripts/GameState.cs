using UnityEngine;

/// <summary>
/// Enum <c>GameStateType</c> contains the different possible game states.
/// </summary>
public enum GameStateType
{
    Initial,
    MainMenu,
    PauseMenu,
    GameOver,
    PreInGame,
    InGame,
}


/// <summary>
/// Class <c>GameState</c> handles the different game states and coordinates
/// between the different states.
/// </summary>
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

    /// <summary>
    /// This method updates the game state and triggers the associated methods 
    /// in order to disable or enable specific menus and overlays. Additionaly
    /// the method also fires the <see cref="OnGameStateChanged"/> action so 
    /// that other scripts can subscribe to the changes.
    /// <param name="newState">The new game state.</param>
    /// </summary>
    public void SetState(GameStateType newState)
    {   
        if (State == newState) return;
        if (State == GameStateType.MainMenu && 
            newState == GameStateType.PauseMenu) return;

        GameStateType previousGameState = State;
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
                TogglePreInGame(previousGameState);
                break;
        }

        OnGameStateChanged?.Invoke(State);
    }

    /// <summary>
    /// This method restets all gameplay relevant stats and updates the current
    /// game state to <see cref="GameStateType.PreInGame"/>.
    /// </summary>
    public void Reset()
    {
        Player.instance.Reset();
        ForceField.instance.Reset();
        Statistics.instance.Reset();
        EnemySpawner.instance.Reset();
        SetState(GameStateType.PreInGame);
    }

    /// <summary>
    /// This method enables the main menu and disables/hides all unnecessary 
    /// informations which are not relevant anymore.
    /// </summary>
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

    /// <summary>
    /// This method enables the pause menu and disables/hides all unnecessary 
    /// informations which are not relevant anymore.
    /// </summary>
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

    /// <summary>
    /// This method enables the game over menu and disables/hides all unnecessary 
    /// informations which are not relevant anymore.
    /// </summary>
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

    /// <summary>
    /// This method gets called if the game state is set to 
    /// <see cref="GameStateType.PreInGame"/>. This intermediate step is necessary
    /// in order to initialize the countdown and waite for it to finish. After 
    /// that the callback method will be called which sets the game state to 
    /// <see cref="GameStateType.InGame"/>. This in turn enables the force field
    /// for the player, shows the holograms and starts the first spawning
    /// process of the enemies.
    /// </summary>
    void TogglePreInGame(GameStateType previousGameState)
    {
        if (previousGameState == GameStateType.PauseMenu) Time.timeScale = 1f;

        StopCountdown();

        XRManager.instance.EnableXRInteractors(false);
        GunManager.instance.EnableGuns(true);

        UIManager.instance.ShowMainMenu(false);
        UIManager.instance.ShowPauseMenu(false);
        UIManager.instance.ShowGameOverMenu(false);

        countdownCoroutine = StartCoroutine(UIManager.instance.StartCountdown(5f, () => {
            SetState(GameStateType.InGame);
            Statistics.instance.ToggleTimer(true);
            UIManager.instance.ShowHolograms(true);
            ForceField.instance.EnableForceField(true);

            UIManager.instance.StopCountdown();
        }));
    }

    /// <summary>
    /// This methods stops the currently running countdown if there is one to stop.
    /// </summary>
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
    /// <see cref="GameStateType.InGame"/>; otherwise, false.
    /// </returns>
    public bool IsInGame
    {
        get { return State == GameStateType.InGame; }
    }

    /// <summary>
    /// This method checks if the player is currently pre in game.
    /// </summary>
    /// <returns>True if the current game state is equals to
    /// <see cref="GameStateType.PreInGame"/>; otherwise, false.
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
