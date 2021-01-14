using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;

    [Header("Spawn Settings")]
    public int startAmount = 5;
    public Vector3 center;
    public Vector3 size;
    public float period = 10.0f;

    private float nextActionTime = 3.0f;

    void Start()
    {
        for (int i = 0; i < startAmount - 1; i++)
        {
            SpawnEnemy();
        }
    } 

    
    void Update()
    {
        if (Time.time > nextActionTime ) {
            this.nextActionTime += period;
            SpawnEnemy();
            // execute block of code here
        }
    } 

    public void SpawnEnemy()
    {
        Vector3  pos = center + new Vector3(
            Random.Range(-size.x / 2, size.x / 2), 
            Random.Range(-size.y / 2, size.y / 2), 
            Random.Range(-size.z / 2, size.z / 2));
     
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.localPosition + center, size);
    }
}
