using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class <c>PauseMenu</c> is used to open the pause menu. From here the user
/// can either restart the current game, go back to the main menu or continue
/// the current game.
/// </summary>

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Freezes the game.
    /// </summary>
    public void OnEnable()
    {
        Statistics.instance.ToggleTimer(false);
    }
    /// <summary>
    /// Unfreezes the game.
    /// </summary>
    public void OnDisable()
    {
        Statistics.instance.ToggleTimer(true);
    }

    /// <summary>
    /// Gets called if the user pressed the continue button which will then set
    /// the <see cref="GameState"/> to <see cref="GameState.IsInGame"/>.
    /// </summary>
    public void OnContinue()
    {
        GameState.instance.SetState(GameStateType.PreInGame);
    }

    /// <summary>
    /// Gets called if the user pressed the exit button which will then set
    /// the <see cref="GameState"/> to <see cref="GameState.IsInGame"/> and load
    /// the main menu scene.
    /// </summary>
    public void ExitToMainMenu()
    {
        //GameState.instance.SetState(GameStateType.MainMenu);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
