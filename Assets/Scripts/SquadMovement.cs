using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMovement : MonoBehaviour {

    private GameObject[] enemies;
    private GameObject closest_enemy;

    private GameObject[] cover_zones;
    private GameObject closest_cover;
    private CoverChecker cover_checker;
    private GameObject[] squaddies = null;
    private float distance = Mathf.Infinity;
    private float rotation_speed = 10.0f;
    private bool in_cover = false;
    private bool is_leader = false;

    public float search_radius = 10.0f;

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
            FollowLeader();
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
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Ally");

        for (int i = 0; i < targets.Length; i++)
        {
            if(targets[i].GetComponent<SquadMovement>().GetLeader() && targets[i] != this.gameObject)
            {
                agent.SetDestination(targets[i].transform.position);
            }

        }


        //move towards the player

    }

    void AttackEnemies()
    {
        
        agent.SetDestination(FindNearestEnemy());
        Debug.Log("Getting Enemies");
    }


    Vector3 FindNearestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, search_radius);
        
        for (int i = 0; i < enemies.Length; i++)
        {
            //if(hitColliders[i].tag == "Enemy")
            //{
                
            //}
            Vector3 diff = enemies[i].transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
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


    public void SetLeader(bool setLeader)
    {
        squaddies = GameObject.FindGameObjectsWithTag("Ally");
        for(int i = 0; i < squaddies.Length; i++)
        {
            if(squaddies[i] != this.gameObject)
            {
                if(squaddies[i].gameObject.GetComponent<SquadMovement>().GetLeader())
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
