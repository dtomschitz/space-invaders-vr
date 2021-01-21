using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

/*public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve fadeCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = fadeCurve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = fadeCurve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        SceneManager.LoadScene(scene);
    }
}*/

public enum FadeDirection
{
    FadeIn,
    FadeOut
}

public class SceneFader : MonoBehaviour
{
    public Volume globalVolume;

    ColorParameter colorParameter;
    IEnumerator coroutine;

    void Awake()
    {
        if (!globalVolume.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            Debug.LogWarning("No color adjustments found!");
        }
        else
        {
            colorParameter = colorAdjustments.colorFilter;
        }
    }

    public void FadeTo(string scene)
    {
        SceneFadeOut();
        SceneManager.LoadScene(scene);
        SceneFadeIn();
    }

    void SceneFadeOut(float time = 2f)
    {
        DoFade(FadeDirection.FadeOut, time);
    }

    void SceneFadeIn(float time = 2f)
    {
        DoFade(FadeDirection.FadeIn, time);
    }

    void DoFade(FadeDirection fadeDirection, float time)
    {
        Color fromColor = Color.white;
        Color toColor = Color.black * -5;

        if (fadeDirection == FadeDirection.FadeIn)
        {
            fromColor = Color.black * -5;
            toColor = Color.white;
        }

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            fromColor = colorParameter.value;
        }

        coroutine = FadeScreen(fromColor, toColor, time);
        StartCoroutine(coroutine);
    }

    IEnumerator FadeScreen(Color from, Color to, float timing)
    {
        colorParameter.value = from;
        float elapsedTime = 0;
        while (elapsedTime < timing)
        {
            colorParameter.Interp(from, to, elapsedTime / timing);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        colorParameter.value = to;
        yield return new WaitForEndOfFrame();
    }
}

