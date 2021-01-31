using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI kills;
    public TextMeshProUGUI time;

    void OnEnable()
    {
        kills.SetText(Statistics.instance.Kills.ToString());
        time.SetText(Statistics.instance.PlayedTime.ToString());
    }

    public void Replay()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

        GameState.instance.SetState(GameStateType.PreInGame);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
