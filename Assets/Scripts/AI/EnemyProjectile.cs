using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyProjectile : MonoBehaviour{

    public float speed;

    private Transform player;

    private Vector3 target;


    void Start(){
        player = GameObject.Find("Body").transform;
        target = new Vector3(player.position.x, player.position.y);
       
    }

    void Update(){
       if(transform.position.z <= 0){
            Destroy(this.gameObject);
            
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

}