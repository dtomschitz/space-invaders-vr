using UnityEngine;

/// <summary>
/// Class <c>EnemyConfig</c> is used to provide an place to define all neccessary
/// paramaters for an specific entity type. This includes the <see cref="EnemyStatsConfig"/>,
/// the <see cref="EnemyCombatConfig"/> and the different amounts of money which
/// could be dropped when the enemy dies as well as the random items.
/// </summary>
[CreateAssetMenu(fileName = "New Enemy Config", menuName = "Configs/Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    [Header("Stats")]
    /// <summary>
    /// The minimum health points a enemy can have.
    /// </summary>
    public float minHealth = 100;
    /// <summary>
    /// The maximum health points a enemy can have.
    /// </summary>
    public float maxHealth = 100;
    /// <summary>
    /// The minimum damage a enemy can deal.
    /// </summary>
    public int minDamage = 5;
    /// <summary>
    /// The maximum damage a enemy can deal.
    /// </summary>
    public int maxDamage = 10;


    [Header("Dodge Settings")]
    /// <summary>
    /// The minimum range of the dodges
    /// </summary>
    public int minDodgeRange = 2;
    /// <summary>
    /// The maximum range of the dodges
    /// </summary>
    public int maxDodgeRange = 2;
    /// <summary>
    /// The minimum speed in which the enemy can dodge.
    /// </summary>
    public float minDodgeSpeed = 1f;
    /// <summary>
    /// The maximum speed in which the enemy can dodge.
    /// </summary>
    public float maxDodgeSpeed = 1.3f;
    /// <summary>
    /// The minimum time of the timer between each dodge
    /// </summary>
    public float minTimeBetweenDodges;
    /// <summary>
    /// The maximum time of the timer between each dodge
    /// </summary>
    public float maxTimeBetweenDodges;

    [Header("Attack Settings")]
    /// <summary>
    /// The minimum start time of the timer between each attack
    /// </summary>
    public float minStartTimeBetweenAttacks;
    /// <summary>
    /// The maximumstart time of the timer between each attack
    /// </summary>
    public float maxStartTimeBetweenAttacks;
    /// <summary>
    /// Toggles if the enemys should attack
    /// </summary>
    public bool disableAttack;
}