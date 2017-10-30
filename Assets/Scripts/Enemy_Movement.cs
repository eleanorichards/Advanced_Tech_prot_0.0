using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Movement : MonoBehaviour {

    private GameObject player;
    public Transform[] patrolPoints; //Add in inspector

    private int patrolPoint = 0;
    private NavMeshAgent agent;
    private Light light;

    void Start()
    {
        light = GetComponentInChildren<Light>();
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();

    }

    void Patrol()
    {
        //if (patrolPoints.Length > 0)
        //{
        //    agent.SetDestination(patrolPoints[patrolPoint].transform.position);
        //    if (transform.position == patrolPoints[patrolPoint].transform.position || Vector3.Distance(transform.position, patrolPoints[patrolPoint].transform.position) < 0.2f)
        //    {
        //        patrolPoint++;    //use distance if needed(lower precision)
        //    }
        //    if (patrolPoint >= patrolPoints.Length)
        //    {
        //        patrolPoint = 0;
        //    }
        //}
    }

    void Attack()
    {
        light.color = Color.red;
        //??? your job...
        //Debug.Log("Whaaaaaaa");    //just to get informed
    }

 

    void Update()
    {
        if (!Physics.Linecast(transform.position, player.transform.position, 1))
        { //check if we see player by linecasting,move player to another layer so the ray won't hit it. 
            Attack();
        }
        else
        {
            light.color = Color.green;

            Patrol();
        }

    }

}
