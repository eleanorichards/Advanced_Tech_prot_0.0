using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour {

    //private Quaternion lock_rotation;

	// Use this for initialization
	void Start () {
        //lock_rotation = transform.rotation;
	}


    void Update()
    {
        transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
    }
}
