using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{

    private int START_AMOUNT = 5;

    public GameObject enenmyPrefab;

    public Vector3 center;
    public Vector3 size;

    private float nextActionTime = 3.0f;
    public float period = 10.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < START_AMOUNT-1; i++)
        {
            SpawnEnemy();
        }


    } 

    
    void Update()
    {

        

        if (Time.time > nextActionTime ) {
            this.nextActionTime = this.nextActionTime + period;
            SpawnEnemy();
            // execute block of code here
        }

        

        
    } 

    public void SpawnEnemy()
    {
        Vector3  pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        
        Instantiate(enenmyPrefab, pos, Quaternion.identity);
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.localPosition + center, size);
    }

}
