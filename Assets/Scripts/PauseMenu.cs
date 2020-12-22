using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
/// <summary>
/// Class <c>PauseMenu</c> is used to open the pause menu. From here the user
/// can either restart the current game, go back to the main menu or continue
/// the current game.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;

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
        GameState.instance.SetState(GameStateType.InGame);
    }

    /// <summary>
    /// Gets called if the user pressed the retry button which will then set
    /// the <see cref="GameState"/> to <see cref="GameState.IsInGame"/> and reload
    /// the main scene.
    /// </summary>
    public void OnRetry()
    {
        GameState.instance.SetState(GameStateType.InGame);
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Gets called if the user pressed the exit button which will then set
    /// the <see cref="GameState"/> to <see cref="GameState.IsInGame"/> and load
    /// the main menu scene.
    /// </summary>
    public void ExitToMainMenu()
    {
        GameState.instance.SetState(GameStateType.InGame);
        sceneFader.FadeTo(menuSceneName);
    }
}

    */