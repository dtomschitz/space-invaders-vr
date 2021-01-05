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
        
        
        if((Vector3.Distance(player.position, transform.position) <= attackRange))
        {
            agent.speed = 0;
            AttackPlayer();
        }else{
            agent.speed = 10;
            ChaseEntity(player.position);
        }
        
        

    }

   
    private void ChaseEntity(Vector3 position)
    {
        agent.SetDestination(position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        
        if(timeBetweenAttacks <= 0){
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBetweenAttacks = startTimeBtwAttacks;
        }else{
            timeBetweenAttacks -= Time.deltaTime;
        }
        

        

        ///attackcode here

    }

}

