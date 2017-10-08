using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverChecker : MonoBehaviour {

    private GameObject coverPoint;

    private GameObject[] enemies;
    //public static GameObject[] cover_blocks;
    private Vector3 position;
    private GameObject closest_enemy;
    private float curDistance;
    public float distance = Mathf.Infinity;
    public bool active = true;

    // Use this for initialization
    void Start () {
        coverPoint = transform.Find("Quad").gameObject;
        position = transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(active)
        {
            //CheckEnemyProximity();
        }
		
	}


    void CheckCoverAvailability()
    {
        
    }


    void CheckEnemyProximity()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            Vector3 diff = enemies[i].transform.position - position;
             curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                closest_enemy = enemies[i];
                distance = curDistance;
            }
        }
        Debug.Log(curDistance);
        Debug.Log(closest_enemy);


    }


    void OnTriggerEnter(Collider col)
    {
        active = false;
        coverPoint.GetComponent<Renderer>().material.color = Color.green;
    }

    void OnTriggerExit(Collider col)
    {
        active = true;
        coverPoint.GetComponent<Renderer>().material.color = Color.red;
    }
}


/*
 * _x;
    private GameObject negCoverPoint_x;
    private GameObject coverPoint_z;
    private GameObject negCoverPoint_z;

  cover_blocks = GameObject.FindGameObjectsWithTag("Cover");

        for (int i = 0; i < cover_blocks.Length; i++)
        {
            coverPoint_x = cover_blocks[i].transform.Find("CoverPoint+x").gameObject;
            negCoverPoint_x = cover_blocks[i].transform.Find("CoverPoint-x").gameObject;
            coverPoint_z = cover_blocks[i].transform.Find("CoverPoint-z").gameObject;
            negCoverPoint_z = cover_blocks[i].transform.Find("CoverPoint+z").gameObject;

        }


    for (int i = 0; i < cover_blocks.Length; i++)
        {
            CheckCoverAvailability(cover_blocks[i]);
        }
 */
