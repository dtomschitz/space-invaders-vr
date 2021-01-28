using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Freezes the game.
    /// </summary>
    public void OnEnable()
    {
        Time.timeScale = 0f;
    }
    /// <summary>
    /// Unfreezes the game.
    /// </summary>
    public void OnDisable()
    {
        Time.timeScale = 1f;
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
