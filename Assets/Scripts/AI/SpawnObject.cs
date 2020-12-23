using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{



    public GameObject enenmyPrefab;

    public Vector3 center;
    public Vector3 size;

    public int objectCount;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy();
        }


    } 

    
    void Update()
    {

        if(enenmyPrefab.transform.position.z <= 0){
            Destroy(this.gameObject);
            
        }

        if (objectCount <= 5 ) 
        {
            
            SpawnEnemy();
        }

        
    } 

    public void SpawnEnemy()
    {
        Vector3  pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        
        Instantiate(enenmyPrefab, pos, Quaternion.identity);
        objectCount++;
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.localPosition + center, size);
    }

}
