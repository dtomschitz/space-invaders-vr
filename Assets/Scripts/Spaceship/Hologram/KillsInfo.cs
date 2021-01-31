using UnityEngine;
using TMPro;

public class KillsInfo : MonoBehaviour
{
    public TextMeshProUGUI killsText;

    void Start()
    {
        Statistics.instance.OnEnemyKilled += OnEnemyKilled;
    }

    /// <summary>
    /// Gets called if the player killed an enemy.
    /// </summary>
    /// <param name="kills">The current amount of kills.</param>
    void OnEnemyKilled(int kills)
    {
        killsText.SetText(kills.ToString());
    }
}
