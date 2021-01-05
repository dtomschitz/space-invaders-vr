
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;





public class EnemyDodges : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsPlayer;

    private bool isdodging;

    public float dodgestart;
    public float dodgetime;
    private bool dodgedirection;

    public int dodgerange;

    private void Awake()
    {
        player = GameObject.Find("Body").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start(){

        agent.speed = 1;
        agent.acceleration = 10;
        

       
    }


    void Update(){

        
       

        if(dodgetime <= 0){

            
            isdodging = true;
            
            if(!dodgedirection){
                Dodge(dodgerange);
            }else{
                Dodge(-dodgerange*2);
            }
            

            dodgetime = dodgestart;


        }else{
            dodgetime -= Time.deltaTime;
            Vector3 destination = agent.transform.position;

            destination.z = destination.z - 1;
            MoveToPosition(destination);
        }

        


    }

    void Dodge(int range){


        Vector3 destination = agent.transform.position;

        destination.x = destination.x + range;

        MoveToPosition(destination);

        dodgedirection = !dodgedirection;
        
        isdodging = false;

    }


    private void MoveToPosition(Vector3 position)
    {
        agent.SetDestination(position);
        if(agent.transform.position == position){
            agent.speed = 0;
        }
    }



}