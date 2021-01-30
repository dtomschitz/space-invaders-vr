using UnityEngine;

/// <summary>
/// Enum <c>WaveState</c> is used to set the current state of the wave spawner.
/// If the current wave state is set to <see cref="WaveState.Spawning"/> the wave spawner is the currently summoning new enemies.
/// The state <see cref="WaveState.Counting"/> will be used if the wave spawner is currently paused and the countdown for the new wave is displayed.
/// When the current wave is still ongoing the wave spawner state will be set to <see cref="WaveState.Running"/>.
/// </summary>
public enum WaveState
{
    Spawning,
    Counting,
    Running
}

/// <summary>
/// Enum <c>Difficulty</c> is used to define the current wave difficulty.
/// Based on the given wave config the difficulity will be used to summon
/// different enemy types and adjust thier health, strengh and weapons.
/// </summary>
public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

/// <summary>
/// Class <c>EnemySpawner</c> spawns the Enemys in the Area of given Size Vector3 size.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public Enemy[] enemyPrefabs;
    public bool isSpawningEnabled;

    [Header("Spawn Settings")]
    public int startAmount = 5;
    public float period = 10.0f;
    public float timeBetweenWaves = 31f;

    [Header("Spawn Cube")]
    public Vector3 center;
    public Vector3 size;

    EnemySpawnerConfig[] configs;
    float nextActionTime = 3.0f;

    private float waveCountdown;
    private float searchCountdown = 1f;

    void Start()
    {
        GameState.instance.OnGameStateChanged += ToggleSpawner;

      /*  for (int i = 0; i < startAmount - 1; i++)
        {
            SpawnEnemy();
        }*/
    } 

    
    void Update()
    {
        if (isSpawningEnabled)
        {
            if (Time.time > nextActionTime)
            {
                this.nextActionTime += period;
                SpawnEnemy();
                // execute block of code here
            }
        }
    } 

    void ToggleSpawner(GameStateType state)
    {
        isSpawningEnabled = state == GameStateType.InGame;
        State = WaveState.Counting;
    }

    /// <summary>
    /// This method resets the current state to <see cref="WaveState.Counting"/>
    /// and the wave countdown to the set initial value.
    /// </summary>
    void ResetWaveSpawner()
    {
        SetState(WaveState.Counting);
        waveCountdown = timeBetweenWaves;
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
        SetState(WaveState.Spawning);

        for (int i = 0; i < Rounds * 1.25; i++)
        {
            Vector3 position = center + new Vector3(
                 Random.Range(-size.x / 2, size.x / 2),
                 Random.Range(-size.y / 2, size.y / 2),
                 Random.Range(-size.z / 2, size.z / 2));

            Enemy enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Enemy enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            if (enemy != null) enemy.Init(CurrentConfig.enemyConfig);

            yield return new WaitForSeconds(1f);
        }
        SetState(WaveState.Running);
        yield break;
    }


    /// <summary>
    /// This method gets called if isSpawningEnabled is true.
    /// </summary>
    void SpawnEnemy()
    {
        Vector3  position = center + new Vector3(
            Random.Range(-size.x / 2, size.x / 2), 
            Random.Range(-size.y / 2, size.y / 2), 
            Random.Range(-size.z / 2, size.z / 2));

        Enemy enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }

    /// <summary>
    /// This methods sets the current state to the new one and will fire the
    /// <see cref="OnWaveStateUpdate"/> event in order to provide the new
    /// state to the shop system and certain ui elements.
    /// </summary>
    /// <param name="newState">The new state.</param>
    void SetState(WaveState newState)
    {
        switch (newState)
        {
            case WaveState.Counting:
                break;

            case WaveState.Running:
                break;

            case WaveState.Spawning:
                break;
        }

        State = newState;
    }

    /// <summary>
    /// This method sets the current <see cref="EnemySpawnerConfig"/> 
    /// to the new one and updates the current difficulty.
    /// </summary>
    /// <param name="config">The new wave config.</param>
    void SetConfig(EnemySpawnerConfig config)
    {
        Debug.LogFormat("Update wave config {0} (round: {1}, difficulty: {2}", config, config.round, config.difficulty);
        CurrentConfig = config;
        CurrentDifficulty = config.difficulty;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }

    /// <summary>
    /// This method determines whether there is enemies are still alive or
    /// not. This is achieved by searching every set time for game objects
    /// with the enemy tag. If an enemy where found the method returns true;
    /// otherwise false.
    /// </summary>
    private bool IsEnemyAlive
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

    public WaveState State { get; protected set; }


    public Difficulty CurrentDifficulty { get; protected set; }

    public EnemySpawnerConfig CurrentConfig { get; protected set; }

    public int Rounds { get; protected set; }

}
