using UnityEngine;

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

    float time;
    bool timer = false;

    void Update()
    {
        if (timer)
        {
            time += Time.deltaTime * 1f;
        }
    }

    public void AddKill() => Kills++;

    public void AddDamage(float damage) => DamageCaused += damage;

    public void ToggleTimer(bool value)
    {
        timer = value;
    }

    /// <summary>
    /// The amount of kills the player made.
    /// </summary>
    public int Kills { get; protected set; }

    /// <summary>
    /// The amount of damage the player dealed.
    /// </summary>
    public float DamageCaused { get; protected set; }

    public string PlayedTime
    {
        get {
            string hours = ((time % 216000) / 3600).ToString("00");
            string minutes = ((time % 3600) / 60).ToString("00");
            string seconds = (time % 60).ToString("00");

            return hours + ":" + minutes + ":" + seconds;
        }
    }
}
