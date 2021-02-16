using UnityEngine;

/// <summary>
/// Class <c>MainMenu</c> is used to open the main menu. From here the user
/// can either start the game or exit it.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Gets called if the user pressed the play button which will then set
    /// the <see cref="GameState"/> to <see cref="GameState.PreInGame"/>.
    /// </summary>
    public void Play()
    {
        GameState.instance.SetState(GameStateType.PreInGame);
    }

    /// <summary>
    /// Gets called if the user pressed the quit button which will then qui the
    /// application.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
