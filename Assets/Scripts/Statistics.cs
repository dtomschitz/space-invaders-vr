using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Class <c>Statistics</c> is used to store different values which can increas
/// will the player is playing. This values will be displayed in the <see cref="GameOver"/>
/// screen when the player finally died.
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

    public void AddKill() 
    {
        Kills++;
        OnEnemyKilled?.Invoke(Kills);
    }

    public void ToggleTimer(bool value)
    {
        isTimerEnabled = value;
    }

    public void ResetTimer()
    {
        ToggleTimer(false);
        time = 0f;
    }

    public void Reset()
    {
        ResetTimer();
        Kills = 0;
    }

    /// <summary>
    /// The amount of kills the player made.
    /// </summary>
    public int Kills { get; protected set; }

    public string PlayedTime
    {
        get => ((time % 3600) / 60).ToString("00") + ":" + (time % 60).ToString("00");
    }
}
