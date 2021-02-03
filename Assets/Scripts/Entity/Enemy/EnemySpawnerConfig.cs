using UnityEngine;

/// <summary>
/// Class <c>EnemySpawnerConfig</c> represents a config file for one or more waves.
/// The config includes the <see cref="round"/> number, the difficulty, the
/// enemy prefab as well as the <see cref="EnemyConfig"/>. Is the current
/// wave count <see cref="WaveSpawner.Rounds"/> equals to set <see cref="round"/>
/// number the specific config gets loaded into the wave spawner and applied to all
/// new created enemies.
/// </summary>
[CreateAssetMenu(fileName = "New Wave Config", menuName = "Configs/Wave Config")]
public class EnemySpawnerConfig : ScriptableObject
{
    /// <summary>
    /// The round on which the specific config should be loaded.
    /// </summary>
    public int round;

    /// <summary>
    /// The round on which the specific config should be loaded.
    /// </summary>
    public int minEnemyCount;

    public int maxEnemyCount;

    public float enemyCountMultiplicator;

    /// <summary>
    /// The actual time between the each wave.
    /// </summary>
    public float timeBetweenWaves = 2.5f;

    /// <summary>
    /// The actual time between each spawning process.
    /// </summary>
    public float timeBetweenSpawns = 1.5f;

    /// <summary>
    /// The configuration for each spawned enemy.
    /// </summary>
    public EnemyConfig enemyConfig;
}
