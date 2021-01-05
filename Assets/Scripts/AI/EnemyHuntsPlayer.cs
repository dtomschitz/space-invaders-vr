using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHuntsPlayer : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsPlayer;

    //Attacking
    public float timeBetweenAttacks;
    public float startTimeBtwAttacks;
    bool alreadyAttacked;
    
    public float attackRange;
    public bool playerInAttackRange;

    public float attacktime;
    public float dodgetime;



    public GameObject projectile;

    private void Awake()
    {
        player = GameObject.Find("Body").transform;
        agent = GetComponent<NavMeshAgent>();
        
    }

    void Start(){
       
        timeBetweenAttacks = startTimeBtwAttacks;
        
    }

    private void Update()
    {

        
        if(transform.position.z <= 0){
            Destroy(agent.gameObject);
            
        }
        //Check for attack range
        transform.LookAt(player.position);
        //print((Vector3.Distance(player.position, transform.position)));
        
        

        if(timeBetweenAttacks <= 0){

            AttackPlayer();

        }else{
            timeBetweenAttacks -= Time.deltaTime;
        }
        
        

    }

 

   


    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        
        Instantiate(projectile, transform.position, Quaternion.identity);
        timeBetweenAttacks = startTimeBtwAttacks;
        

        

        ///attackcode here

    }

}

