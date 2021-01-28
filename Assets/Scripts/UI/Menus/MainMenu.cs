using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        GameState.instance.SetState(GameStateType.PreInGame);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
