using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPoint : MonoBehaviour {
    private LayerMask ground;
    Light hit_light;
    public GameObject hit_Object;

    public float max_distance = 100;

	// Use this for initialization
	void Start () {
        ground = LayerMask.GetMask("Ground");
        //hit_light = GetComponent <Light> ();

	}
	
	// Update is called once per frame
	void Update ()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        //this will only change hit_Object's position if the raycast is hitting the floor layer mask
        if (Physics.Raycast(transform.position, fwd, out hit, max_distance, ground))
        {         
            //Vector3 hit_position = transform.InverseTransformPoint(hit.point);
            //Debug.Log(hit.point);
            hit_Object.transform.position = hit.point;
        }
    }
}
