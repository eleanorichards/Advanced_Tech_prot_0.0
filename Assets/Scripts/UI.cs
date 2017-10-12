using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public GameObject Canvas;

    public Text centreDisplay;

	// Use this for initialization
	void Start () {
        //INVESTIGATION font
        //FindCover = GameObject.Find("FindCover");
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            centreDisplay.text = "Follow Leader!";
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            centreDisplay.text = "Follow Leader!";
        }
        if (Input.GetKeyDown("Fire1"))
        {
            centreDisplay.text = "Follow Leader!";
        }

    }
}
