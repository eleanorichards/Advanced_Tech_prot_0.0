using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverChecker : MonoBehaviour {

    private GameObject coverPoint;
    private GameObject[] enemies;
    private GameObject closest_enemy;

    private float curDistance;
    public float distance = Mathf.Infinity;
    private Vector3 position;
    public bool active = true;

    private Renderer obj_renderer;
    // Use this for initialization
    void Start () {
        //coverPoint = transform.Find("Quad").gameObject;
        position = transform.position;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //coverManager = GetComponentInParent<CoverManager>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        //if(active)
        //{
        //    //CheckEnemyProximity();
        //}
        //InvokeRepeating("CheckEnemyProximity", 1.0f, 1.0f);
		
	}




    void OnTriggerStay(Collider col)
    {
        active = false;
    }

    
    public bool isActive()
    {
        return active;
    }
}
