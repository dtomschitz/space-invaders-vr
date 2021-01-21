using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownController : MonoBehaviour
{
    public float time;
    public TextMeshProUGUI countdownText;

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (time > 0)
        {
            countdownText.text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }

        countdownText.text = "GO!";
        GameState.instance.SetState(GameStateType.InGame);

        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }
}
