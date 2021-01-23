using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public VRFader sceneFader;

    public void Play()
    {
        GameState.instance.SetState(GameStateType.PreInGame);
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
