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
    bool alreadyAttacked;

    public float attackRange;
    public bool playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Body").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(transform.position.z <= 0){
            Destroy(agent.gameObject);
        }
        //Check for attack range
        transform.LookAt(player.position);
        print(Vector3.Distance(player.position, transform.position));
        playerInAttackRange = (Vector3.Distance(player.position, transform.position)<= attackRange);

        if(playerInAttackRange)
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
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        ///attackcode here

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}

