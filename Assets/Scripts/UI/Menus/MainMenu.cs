using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public VRFader sceneFader;

    public void Play()
    {
      // sceneFader.FadeTo(level);
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
