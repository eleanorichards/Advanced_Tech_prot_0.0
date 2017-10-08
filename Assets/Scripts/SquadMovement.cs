using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMovement : MonoBehaviour {

    private Vector3 goal = new Vector3(-5.5f, 0.0f, 2.2f);


    private GameObject[] enemies;
    private GameObject closest_enemy;

    private GameObject[] cover_zones;
    private GameObject closest_cover;
    private float distance = Mathf.Infinity;

    private bool in_cover = false;

    UnityEngine.AI.NavMeshAgent agent;

    // Use this for initialization
    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal;
        
    }
	
	// Update is called once per frame
	void Update () {
        if(!in_cover)
        {
            //FindNearestCover();
            //agent.destination = closest_cover.transform.position;
            agent.SetDestination(FindNearestCover());
        }

    }


    void FindNearestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            Vector3 diff = enemies[i].transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            float distance = 0.0f;
            if (curDistance < distance)
            {
                closest_enemy = enemies[i];
                distance = curDistance;
            }
            Debug.Log(curDistance);
            Debug.Log(closest_enemy);
        }
    }


    Vector3 FindNearestCover()
    {
        cover_zones = GameObject.FindGameObjectsWithTag("Cover");

        for (int i = 0; i < cover_zones.Length; i++)
        {
            Vector3 diff = cover_zones[i].transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest_cover = cover_zones[i];
                distance = curDistance;
            }
            Debug.Log(curDistance);
            Debug.Log(closest_cover);
        }
        return closest_cover.transform.position;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Cover")
        {
            in_cover = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Cover")
        {
            in_cover = false;
        }
    }
}
