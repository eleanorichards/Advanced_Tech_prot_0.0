using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverManager : MonoBehaviour {
    public bool cover_active = true;

    private GameObject[] cover_points;

    private GameObject this_cover_point;
    private GameObject cover_x;
    private GameObject cover_z;
    private GameObject neg_cover_x;
    private GameObject neg_cover_z;


    // Use this for initialization
    void Start () {

        cover_points = GameObject.FindGameObjectsWithTag("Cover");

        for(int i = 0; i < cover_points.Length; i++)
        {

            if(cover_points[i].transform.IsChildOf(transform))
            {
                if(cover_points[i].name == "CoverPoint-x")
                {
                    neg_cover_x = cover_points[i];
                }
                if (cover_points[i].name == "CoverPoint+x")
                {
                    cover_x = cover_points[i];
                }
                if (cover_points[i].name == "CoverPoint-z")
                {
                    neg_cover_z = cover_points[i];
                }
                if (cover_points[i].name == "CoverPoint+z")
                {
                    cover_x = cover_points[i];
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        //if(col.gameObject.tag == "Cover")
        //{
        //    cover_active = false;
        //}
    }

    public void SetCoverPointActive(GameObject coverPoint)
    {
        //Debug.Log("point Activated" + coverPoint.transform.name);
    }


    public void SetCoverPointInactive(GameObject coverPoint)
    {
        //Debug.Log("point Deactivated" + coverPoint.transform.name);

    }
}
