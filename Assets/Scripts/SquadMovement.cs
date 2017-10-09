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
    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        cover_zones = GameObject.FindGameObjectsWithTag("Cover");
    }

    // Update is called once per frame
    void Update () {
        if(!in_cover)
        {
            Debug.Log("searching");

            //FindNearestCover();
            //agent.destination = closest_cover.transform.position;
            agent.SetDestination(FindNearestCover());
        }
        else
        {
            Debug.Log("cover foudn. Standing for orders");
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

        for (int i = 0; i < cover_zones.Length; i++)
        {
            //cover_checker = cover_zones[i].GetComponent<CoverChecker>();
            //if (cover_checker.pointActive())
           // {
                Vector3 diff = cover_zones[i].transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest_cover = cover_zones[i];
                    distance = curDistance;
                }
                //Debug.Log(curDistance);
                //Debug.Log(closest_cover);
          //  }
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
