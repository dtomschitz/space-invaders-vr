using UnityEngine;
using TMPro;

public class TimeInfo : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Update()
    {
        timeText.SetText(Statistics.instance.PlayedTime);
    }
}
