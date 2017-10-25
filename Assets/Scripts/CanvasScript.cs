using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {


    public Text centreDisplay;
    public Image crosshair;
    private GameObject[] Squad = new GameObject[100];
    private string view_state = "";
    
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
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            StartCoroutine(ShowMessage("Form Line!", 1.0f));
            SendMessageToSquad("Form Line");
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            StartCoroutine(ShowMessage("Form V!", 1.0f));
            SendMessageToSquad("Form V");
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


    public void SetCrosshairState(string _state)
    {
        view_state = _state;
        if(view_state == "Default")
        {
            Debug.Log("defaulting");
            crosshair.color = Color.green;
        }
        else if (view_state == "Ally")
        {
            Debug.Log("ally in view");
            crosshair.color = Color.blue;
        }
        else if (view_state == "Enemy")
        {
            Debug.Log("ally in view");
            crosshair.color = Color.red;
        }
    }
}
