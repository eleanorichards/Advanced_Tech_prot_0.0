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
        centreDisplay.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine(ShowMessage("Follow Leader!", 1.0f));
            SendMessageToSquad("Follow Leader");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            StartCoroutine(ShowMessage("Find Cover!", 1.0f));
            SendMessageToSquad("Find Cover");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            StartCoroutine(ShowMessage("Attack!", 1.0f));
            SendMessageToSquad("Attack");
        }
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        centreDisplay.text = message;
        centreDisplay.enabled = true;
        yield return new WaitForSeconds(delay);
        centreDisplay.enabled = false;

    }
    void SendMessageToSquad(string message)
    {
        Squad = GameObject.FindGameObjectsWithTag("Ally");
        for(int i = 0; i < Squad.Length; i++)
        {
            Squad[i].GetComponent<SquadMovement>().ActivateState(message);
        }
    }
}
