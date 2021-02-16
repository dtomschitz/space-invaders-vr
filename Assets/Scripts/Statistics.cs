using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class <c>Statistics</c> is used to store different values which can increas
/// will the player is playing. This values will be displayed in the 
/// <see cref="GameOver"/> screen when the player finally died or in the holograms
/// while he is playing.
/// </summary>
public class Statistics : MonoBehaviour
{
    #region Singelton

    public static Statistics instance;

    void Awake()
    {
        instance = this;
    }

    #endregion;

    public UnityAction<int> OnEnemyKilled;

    float time;
    bool isTimerEnabled = false;

    void Update()
    {
        if (isTimerEnabled)
        {
            time += Time.deltaTime * 1f;
        }
    }

    /// <summary>
    /// Adds a new kill to the statistics and emits the <see cref="OnEnemyKilled"/>
    /// action which is used to update the holograms.
    /// </summary>
    public void AddKill() 
    {
        Kills++;
        OnEnemyKilled?.Invoke(Kills);
    }

    /// <summary>
    /// Enables or disables the timer.
    /// </summary>
    public void ToggleTimer(bool value)
    {
        isTimerEnabled = value;
    }

    /// <summary>
    /// Resets the timer.
    /// </summary>
    public void ResetTimer()
    {
        ToggleTimer(false);
        time = 0f;
    }

    /// <summary>
    /// Resets the current timer and the kill count.
    /// </summary>
    public void Reset()
    {
        ResetTimer();
        Kills = 0;
    }

    /// <summary>
    /// The amount of kills the player made.
    /// </summary>
    /// <returns>The kill counter.</returns>
    public int Kills { get; protected set; }

    /// <summary>
    /// The time the player actually stayed alive.
    /// </summary>
    /// <returns>The play time.</returns>
    public string PlayedTime
    {
        get => ((time % 3600) / 60).ToString("00") + ":" + (time % 60).ToString("00");
    }
}
