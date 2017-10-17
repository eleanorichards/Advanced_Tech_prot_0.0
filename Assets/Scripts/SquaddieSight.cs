﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquaddieSight : MonoBehaviour {
    public float FOV = 65.0f;
    public float immediate_range = 5.0f;
    private Vector3 ray_direction = Vector3.zero;
    public float visibilty_range = 25.0f;
    private GameObject enemy;

    public bool enemy_in_view = false;
    public bool enemy_near = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CanSeePlayer();
	}

    public void CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 rayDirection = Vector3.zero;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, immediate_range);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Enemy")
            {
                enemy_near = true;
                enemy = hitColliders[i].gameObject;
                Debug.Log("Enemy in immediate range");
            }
        }
        if(enemy)
        {
            rayDirection = enemy.transform.position - transform.position;
        }

        if ((Vector3.Angle(rayDirection, transform.forward)) <= FOV * 0.5f)
        {
            // Detect if player is within the field of view
            if (Physics.Raycast(transform.position, transform.forward, out hit, visibilty_range))
            {
                Debug.DrawRay(transform.position, transform.forward, Color.red, visibilty_range);
                if (hit.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy in view");
                }
            }
        }       

    }

}