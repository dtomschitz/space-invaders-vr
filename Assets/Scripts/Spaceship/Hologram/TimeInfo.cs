using UnityEngine;
using TMPro;

/// <summary>
/// Class <c>TimeInfo</c> is used to display and update the elapsed time on the
/// right hologram.
/// </summary>
public class TimeInfo : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    void Update()
    {
        timeText.SetText(Statistics.instance.PlayedTime);
    }
}
