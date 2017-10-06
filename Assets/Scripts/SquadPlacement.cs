using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadPlacement : MonoBehaviour {

    public GameObject SquadMember;
    private LayerMask ground;
    private float max_distance = 2.0f;
    private float cast_radius = 0.2f;

	// Use this for initialization
	void Start () {
        ground = LayerMask.GetMask("Default");
	}
	
	// Update is called once per frame
	void Update () {
       
    }


    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Wall")
        {
            Debug.Log("wALL HIT");
            RaycastHit hit;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 down = transform.TransformDirection(-Vector3.up);

            //if (Physics.Raycast(transform.position, down, out hit, max_distance, ground))
            if (Physics.SphereCast(transform.position, cast_radius, down, out hit, max_distance, ground))
            {              
                Debug.Log("Squaddie Inserted ;)");
                //Vector3 hit_position = transform.InverseTransformPoint(hit.point);
                SquadMember.transform.position = hit.point;
                Vector3 Squaddie_pos = transform.position;
                //Debug.Log(hit.point);
                Instantiate(SquadMember, transform.position, transform.rotation);
            }
        }
        
    }
}
