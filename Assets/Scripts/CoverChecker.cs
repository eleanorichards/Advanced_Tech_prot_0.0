using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverChecker : MonoBehaviour {

    private GameObject coverPoint;
    private GameObject[] enemies;
    private CoverManager coverManager;
    private GameObject closest_enemy;

    private float curDistance;
    public float distance = Mathf.Infinity;
    private Vector3 position;
    public bool active = true;

    private Renderer obj_renderer;
    // Use this for initialization
    void Start () {
        coverPoint = transform.Find("Quad").gameObject;
        position = transform.position;
        obj_renderer = coverPoint.GetComponent<Renderer>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //coverManager = GetComponentInParent<CoverManager>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        //if(active)
        //{
        //    //CheckEnemyProximity();
        //}
        InvokeRepeating("CheckEnemyProximity", 1.0f, 1.0f);
		
	}


    void CheckCoverAvailability()
    {
        
    }


    void CheckEnemyProximity()
    {

        for (int i = 0; i < enemies.Length; i++)
        {
            Vector3 diff = enemies[i].transform.position - position;
            curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest_enemy = enemies[i];
                distance = curDistance;
            }
            Debug.Log(curDistance);
            setWarningZone(distance);
        }

    }


    void setWarningZone(float enemyRange)
    {
        if(enemyRange <= 10.0f)
        {
            Debug.Log("Enemy Close");
            obj_renderer.material.color = Color.red;
        }
        else if (enemyRange <= 20.0f)
        {
            Debug.Log("Enemy Closish");

            obj_renderer.material.color = Color.yellow;
        }
        else if (enemyRange <= 30.0f)
        {
            Debug.Log("Enemy Far");

            obj_renderer.material.color = Color.green;
        }
    }


    void OnTriggerEnter(Collider col)
    {
        active = false;
        //coverPoint.GetComponent<Renderer>().material.color = Color.green;
        //coverManager.SetCoverPointInactive(gameObject);
    }

    void OnTriggerExit(Collider col)
    {
        active = true;
        //coverPoint.GetComponent<Renderer>().material.color = Color.red;
       //coverManager.SetCoverPointActive(gameObject);

    }


    public bool pointActive()
    {
        return active;
    }
}
