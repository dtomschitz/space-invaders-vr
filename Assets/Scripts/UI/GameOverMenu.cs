using UnityEngine;
using TMPro;

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
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        GameState.instance.Reset();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
