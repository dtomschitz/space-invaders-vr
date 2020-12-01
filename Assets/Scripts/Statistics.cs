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

    /// <summary>
    /// The amount of kills the player made.
    /// </summary>
    public int Kills { get; protected set; }

    /// <summary>
    /// The amount of damage the player dealed.
    /// </summary>
    public float DamageCaused { get; protected set; }

    public void AddKill() => Kills++;
    public void AddDamage(float damage) => DamageCaused += damage;
}
