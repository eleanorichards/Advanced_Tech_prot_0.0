using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMovement : MonoBehaviour {

    private GameObject[] enemies;
    private GameObject closest_enemy;

    private GameObject[] cover_zones;
    private GameObject closest_cover;
    private CoverChecker cover_checker;

    private float distance = Mathf.Infinity;

    private bool in_cover = false;

    UnityEngine.AI.NavMeshAgent agent;

    // Use this for initialization
    void Start() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        cover_zones = GameObject.FindGameObjectsWithTag("Cover");

    }

    // Update is called once per frame
    void Update() {


    }

    public void ActivateState(string statename)
    {
        if (statename == "Find Cover")
        {
            Debug.Log("running");
            RunToCover();
        }
        else if (statename == "Follow Leader")
        {

        }
        else if (statename == "Attack Enemies")
        {
            AttackEnemies();
        }
    }


    void RunToCover()
    {
        if (!in_cover)
        {
            agent.SetDestination(FindNearestCover());
        }
        else
        {
            Debug.Log("cover found. Standing for orders");
        }
    }


    void FollowLeader()
    {

    }

    void AttackEnemies()
    {
        agent.SetDestination(FindNearestEnemy());
        Debug.Log("Getting Enemies");
    }


    Vector3 FindNearestEnemy()
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
        }
        return closest_enemy.transform.position;
    }


    Vector3 FindNearestCover()
    {

        for (int i = 0; i < cover_zones.Length; i++)
        {

            Vector3 diff = cover_zones[i].transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest_cover = cover_zones[i];
                distance = curDistance;
            }

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
