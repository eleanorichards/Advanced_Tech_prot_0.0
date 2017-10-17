﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMovement : MonoBehaviour
{

    private GameObject[] enemies;
    private GameObject closest_enemy;
    private GameObject[] cover_zones;
    private GameObject closest_cover;
    private GameObject[] squaddies = null;

    private float distance = Mathf.Infinity;
    private float rotation_speed = 10.0f;
    private bool in_cover = false;
    private bool is_leader = false;
    private bool following_leader = false;

    private string statename = "";
    private int zone_taken = -1;
    private int enemy_num = 0;
    public float search_radius = 10.0f;
    public float bump_radius = 1.0f;
    public float immediate_range = 15.0f;

    UnityEngine.AI.NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        cover_zones = GameObject.FindGameObjectsWithTag("Cover");
    }

    // Update is called once per frame
    void Update()
    {
        if(following_leader)
        {
            FollowLeader();
            Debug.Log("following");
        }
        if(!following_leader || is_leader)
        {
            if (statename == "Find Cover")
            {
                RunToCover();
            }          
            else if (statename == "Attack")
            {
                AttackEnemies();
            }
        }


    }

    public void ActivateState(string _statename)
    {
        statename = _statename;

        if (statename == "Follow Leader")
        {
            if(following_leader)
            {
                following_leader = false;
            }
            else if(!following_leader)
            {
                following_leader = true;
            }
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

        }
    }


    void FollowLeader()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Ally");

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].GetComponent<SquadMovement>().GetLeader() && targets[i] != this.gameObject)
            {
                Vector3 distance = targets[i].transform.position - transform.position;
                float curDistance = distance.sqrMagnitude;
                if (curDistance > bump_radius)
                {
                    Debug.Log(curDistance);
                    agent.SetDestination(targets[i].transform.position);
                }
            }

        }
    }

    void AttackEnemies()
    {
        FindNearestEnemy();
        Debug.Log("Getting Enemies");
    }


    void FindNearestEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, immediate_range);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Enemy")
            {
                enemy_num++;
                enemies[enemy_num] = hitColliders[i].gameObject;
            }

        }
        for (int i = 0; i < enemy_num; i++)
        {
            Debug.Log("searching" + enemy_num);
            Vector3 diff = enemies[i].transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest_enemy = enemies[i];
                distance = curDistance;
                agent.SetDestination(closest_enemy.transform.position);
            }
        }
        
    }


    Vector3 FindNearestCover()
    {
        for (int i = 0; i < cover_zones.Length; i++)
        {
            Vector3 diff = cover_zones[i].transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && i != zone_taken)
            {
                closest_cover = cover_zones[i];
                distance = curDistance;
                zone_taken = i;
            }
        }
        return closest_cover.transform.position;
    }


    public void SetLeader(bool setLeader)
    {
        squaddies = GameObject.FindGameObjectsWithTag("Ally");
        for (int i = 0; i < squaddies.Length; i++)
        {
            if (squaddies[i] != this.gameObject)
            {
                if (squaddies[i].gameObject.GetComponent<SquadMovement>().GetLeader())
                {
                    squaddies[i].gameObject.GetComponent<SquadMovement>().SetLeader(false);
                }
            }
        }
        is_leader = setLeader;
        Debug.Log("set a leader");
    }


    public bool GetLeader()
    {
        return is_leader;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Cover")
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


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //enemy damage script
            Destroy(col.gameObject);
        }
    }
}
