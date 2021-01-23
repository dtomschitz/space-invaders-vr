using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public float time;
    public TextMeshProUGUI countdownText;

    public IEnumerator StartCountdown(Action callback)
    {
        while (time > 0)
        {
            countdownText.text = time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }

        countdownText.text = "GO!";
        callback();
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }
}
