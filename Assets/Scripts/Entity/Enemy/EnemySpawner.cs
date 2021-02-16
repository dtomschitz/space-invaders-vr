using UnityEngine;
using System.Collections;

/// <summary>
/// Enum <c>WaveState</c> is used to set the current state of the wave spawner.
/// If the current wave state is set to <see cref="EnemySpawnerState.Spawning"/> 
/// the wave spawner is the currently summoning new enemies. The state 
/// <see cref="EnemySpawnerState.Counting"/> will be used if the wave spawner 
/// is currently paused and the countdown for the new wave is displayed.
/// When the current wave is still ongoing the wave spawner state will be set 
/// to <see cref="EnemySpawnerState.Running"/>.
/// </summary>
public enum EnemySpawnerState
{
    Disabled,
    Spawning,
    Counting,
    Running
}

/// <summary>
/// Class <c>EnemySpawner</c> spawns the Enemys in the Area of given Size Vector3 size.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    #region Singelton

    public static EnemySpawner instance;

    void Awake()
    {
        instance = this;
    }

    #endregion;

    public Enemy[] enemyPrefabs;
    public bool isSpawningEnabled;

    [Header("Spawn Cube")]
    public Vector3 center;
    public Vector3 size;

    public EnemySpawnerConfig[] configs;
    float waveCountdown;
    float searchCountdown = 1f;

    void Start()
    {
        GameState.instance.OnGameStateChanged += ToggleSpawner;
        configs = Resources.LoadAll<EnemySpawnerConfig>("WaveConfigs");
        Debug.LogFormat("Loaded {0} spawner configs.", configs.Length);
    } 
    
    void Update()
    {
        if (isSpawningEnabled && State != EnemySpawnerState.Disabled)
        {
            if (State == EnemySpawnerState.Running)
            {
                if (IsEnemyAlive) return;
                ResetWaveSpawner();
            }

            if (waveCountdown <= 0f)
            {
                waveCountdown = 0f;
                if (State != EnemySpawnerState.Spawning) StartNextWave();
                return;
            }

            waveCountdown -= Time.deltaTime;
        }
    }

    public void Reset()
    {
        Rounds = 0;
    }

    /// <summary>
    /// Enables or disables the <see cref="EnemySpawner"/> based on the current
    /// game state. If the new game state is set to GameOver the method will
    /// also kill all enemies and the projectiles.
    /// </summary>
    void ToggleSpawner(GameStateType newState)
    {
        bool isInGame = newState == GameStateType.InGame;
        isSpawningEnabled = isInGame;
        State = isInGame ? EnemySpawnerState.Counting : EnemySpawnerState.Disabled;

        if (newState == GameStateType.GameOver)
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }

            foreach (GameObject enemyProjectile in GameObject.FindGameObjectsWithTag("EnemyProjectile"))
            {
                Destroy(enemyProjectile);
            }
        }
    } 

    /// <summary>
    /// This method starts the next wave. It will add a new round to the
    /// <see cref="WaveSpawner.Rounds"/> counter, select the specific config
    /// which should be used for the new wave and will summon the new enemies.
    /// </summary>
     void StartNextWave()
    {
        EnemySpawnerConfig nextConfig = GetNextEnemySpawnerConfig();
        if (nextConfig != null) SetConfig(nextConfig);

        Rounds++;
        Debug.LogFormat("Spawning Wave (num: {0})", Rounds);

        StartCoroutine(SpawnRoutine());
    }

    /// <summary>
    /// This method resets the current state to <see cref="EnemySpawnerState.Counting"/>
    /// and the wave countdown to the set initial value.
    /// </summary>
    void ResetWaveSpawner()
    {
        SetState(EnemySpawnerState.Counting);
        waveCountdown = CurrentConfig.timeBetweenWaves;
    }

    /// <summary>
    /// This method spawns the enemies for the current wave. For each enemy
    /// one of the set spawn points will be selected randomly. The new
    /// instantiate enemy will then get the enemy config of the current wave
    /// config in order to create random enemy types.
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnRoutine()
    {
        SetState(EnemySpawnerState.Spawning);

        int count = Random.Range(CurrentConfig.minEnemyCount, CurrentConfig.maxEnemyCount);
        for (int i = 0; i < count * CurrentConfig.enemyCountMultiplicator; i++)
        {
            Vector3 position = center + new Vector3(
                 Random.Range(-size.x / 2, size.x / 2),
                 Random.Range(-size.y / 2, size.y / 2),
                 Random.Range(-size.z / 2, size.z / 2));

            Enemy enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Enemy enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            if (enemy != null) enemy.Init(CurrentConfig.enemyConfig);

            yield return new WaitForSeconds(CurrentConfig.timeBetweenSpawns);
        }

        SetState(EnemySpawnerState.Running);
        yield break;
    }

    /// <summary>
    /// This methods sets the current state to the new one and will fire the
    /// <see cref="OnWaveStateUpdate"/> event in order to provide the new
    /// state to the shop system and certain ui elements.
    /// </summary>
    /// <param name="newState">The new state.</param>
    void SetState(EnemySpawnerState newState)
    {
        State = newState;
    }

    /// <summary>
    /// This method sets the current <see cref="EnemySpawnerConfig"/> 
    /// to the new one and updates the current difficulty.
    /// </summary>
    /// <param name="config">The new wave config.</param>
    void SetConfig(EnemySpawnerConfig config)
    {
        Debug.LogFormat("Update wave config {0} (round: {1})", config, config.round);
        CurrentConfig = config;
    }

    /// <summary>
    /// This method determinants which of the set <see cref="EnemySpawner.configs"/>
    /// configs should be used for the next wave and returns it.
    /// </summary>
    /// <returns>The wave config which is intended for the current round number.</returns>
    EnemySpawnerConfig GetNextEnemySpawnerConfig()
    {
        foreach (EnemySpawnerConfig config in configs)
        {
            if (config.round == Rounds) return config;
        }
        return null;
    }

    /// <summary>
    /// This method determines whether there is enemies are still alive or
    /// not. This is achieved by searching every set time for game objects
    /// with the enemy tag. If an enemy where found the method returns true;
    /// otherwise false.
    /// </summary>
    bool IsEnemyAlive
    {
        get
        {
            searchCountdown -= Time.deltaTime;
            if (searchCountdown <= 0f)
            {
                searchCountdown = 1f;
                if (GameObject.FindGameObjectWithTag("Enemy") == null) return false;
            }
            return true;
        }
    }

    /// <summary>
    /// This method returns the current enemy spawner state.
    /// </summary>
    /// <returns>The current enemy spawner state.</returns>
    public EnemySpawnerState State { get; protected set; }

    public EnemySpawnerConfig CurrentConfig { get; protected set; }

    public int Rounds { get; protected set; } = 0;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
