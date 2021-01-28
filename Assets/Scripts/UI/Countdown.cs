using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;

    public IEnumerator StartCountdown(float time, Action callback)
    {
        gameObject.SetActive(true);

        while (time > 0)
        {
            countdownText.text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }

        countdownText.text = "GO";
        callback();
        yield return new WaitForSeconds(1f);
        StopCountdown();
    }

    public void StopCountdown()
    {
        countdownText.text = "";
        gameObject.SetActive(false);
    }
}
