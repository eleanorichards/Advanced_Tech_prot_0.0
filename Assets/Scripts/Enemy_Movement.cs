using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour {

    public Transform[] goals;
    UnityEngine.AI.NavMeshAgent agent;
    int i = 0;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update ()
    {
		if(transform.position == goals[i].position)
        {
            if(i < goals.Length)
            {
                i++;
                agent.destination = goals[i].position;

            }
            else
            {
                i--;
                agent.destination = goals[i].position;
            }
        }

    }

    
}
