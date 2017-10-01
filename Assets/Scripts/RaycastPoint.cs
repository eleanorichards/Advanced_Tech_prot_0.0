using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPoint : MonoBehaviour {
    private LayerMask ground;
    Light hit_light;

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

        if (Physics.Raycast(ray, out hit, max_distance, ground))
        {         
            Vector3 hit_position = transform.InverseTransformPoint(hit.point);
            Debug.Log(hit_position);
            transform.position = hit.point;
        }
    }
}
