using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public GameObject Canvas;

    public Text centreDisplay;
    private GameObject[] Squad;

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
            SendMessageToSquad("Follow Leader");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            centreDisplay.text = "Find Cover!";
            SendMessageToSquad("Find Cover");

        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            centreDisplay.text = "Attack!";
            SendMessageToSquad("Attack");
        }
        else
        {
            centreDisplay.text = "";
        }

    }


    void SendMessageToSquad(string message)
    {
        Squad = GameObject.FindGameObjectsWithTag("Ally");
        for(int i = 0; i < Squad.Length; i++)
        {
            Squad[i].GetComponent<SquadMovement>().ActivateState(message);
            Debug.Log(message + "sent");
        }
    }
}
