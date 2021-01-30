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
    /// The maximum health points a enemy can have.
    /// </summary>
    public float maxHealth;
    /// <summary>
    /// The maximum damage a enemy can deal.
    /// </summary>
    public float damage;

    [Header("Dodge Settings")]
    /// <summary>
    /// The range of the dodges
    /// </summary>
    public int dodgeRange;
    /// <summary>
    /// The start time of the timer between each dodge
    /// </summary>
    public float startTimeBetweenDodges;
    /// <summary>
    /// The actual time of the timer between each dodge
    /// </summary>
    public float timeBetweenDodges;

    [Header("Attack Settings")]
    /// <summary>
    /// The actual time of the timer between each attack
    /// </summary>
    public float timeBetweenAttacks;
    /// <summary>
    /// The start time of the timer between each attack
    /// </summary>
    public float startTimeBetweenAttacks;
    /// <summary>
    /// Toggles if the enemys should attack
    /// </summary>
    public bool disableAttack;
}