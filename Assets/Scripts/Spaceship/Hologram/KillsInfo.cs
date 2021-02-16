using UnityEngine;
using TMPro;

/// <summary>
/// Class <c>KillsInfo</c> is used to display and update the kill counter on the
/// left hologram.
/// </summary>
public class KillsInfo : MonoBehaviour
{
    public TextMeshProUGUI killsText;

    void Start()
    {
        Statistics.instance.OnEnemyKilled += OnEnemyKilled;
    }

    /// <summary>
    /// Gets called if the player killed an enemy in order to update the kill
    /// counter on the hologram.
    /// </summary>
    /// <param name="kills">The current amount of kills.</param>
    void OnEnemyKilled(int kills)
    {
        killsText.SetText(kills.ToString());
    }
}
