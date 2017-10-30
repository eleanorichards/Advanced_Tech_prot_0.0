using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour {

    private GameObject player;
    public Vector3 offset = new Vector3(0.0f, 7.0f, 0.0f);

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
    }


    void Update()
    {
        transform.position = player.transform.position + offset;

    }
}
