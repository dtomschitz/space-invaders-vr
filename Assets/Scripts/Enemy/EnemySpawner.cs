using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] enemyPrefabs;
    public bool isSpawningEnabled;

    [Header("Spawn Settings")]
    public int startAmount = 5;
    public Vector3 center;
    public Vector3 size;
    public float period = 10.0f;

    float nextActionTime = 3.0f;

    void Start()
    {
        GameState.instance.OnGameStateChanged += ToggleSpawner;
    } 

    
    void Update()
    {
        if (isSpawningEnabled)
        {
            if (Time.time > nextActionTime)
            {
                this.nextActionTime += period;
                SpawnEnemy();
            }
        }
    } 

    void ToggleSpawner(GameStateType state)
    {
        isSpawningEnabled = state == GameStateType.InGame;
    }

    void SpawnEnemy()
    {
        Vector3  position = center + new Vector3(
            Random.Range(-size.x / 2, size.x / 2), 
            Random.Range(-size.y / 2, size.y / 2), 
            Random.Range(-size.z / 2, size.z / 2));

        Enemy enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
