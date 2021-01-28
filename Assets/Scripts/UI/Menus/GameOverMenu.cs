using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
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
