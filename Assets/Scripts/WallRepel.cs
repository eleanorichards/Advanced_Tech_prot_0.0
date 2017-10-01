using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRepel : MonoBehaviour {

    public Vector2 repel_force = Vector2.zero;
    public float force_magnitude = 30.0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Entered");
            Vector3 direction = col.contacts[0].point - transform.position;
            direction = -direction.normalized;
            direction.y = 0;
            col.gameObject.GetComponent<Rigidbody>().AddForce(-direction * force_magnitude, ForceMode.Impulse);
        }   
    }

    void OnCollisionExit2D(Collision2D col)
    {
        repel_force = Vector2.zero;
    }
}


