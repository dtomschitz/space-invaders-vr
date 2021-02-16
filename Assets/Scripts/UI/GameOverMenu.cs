using UnityEngine;
using TMPro;

/// <summary>
/// Class <c>GameOverMenu</c> is used to open the game over menu. From here the 
/// user can either restart the game or exit it.
/// </summary>
public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI kills;
    public TextMeshProUGUI time;
    
    void OnEnable()
    {
        kills.SetText(Statistics.instance.Kills.ToString());
        time.SetText(Statistics.instance.PlayedTime.ToString());
    }

    /// <summary>
    /// Gets called if the user pressed the restart button which will then 
    /// reset the game state in order to restart the game.
    /// </summary>
    public void Replay()
    {
        GameState.instance.Reset();
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
