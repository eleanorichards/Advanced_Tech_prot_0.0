using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {


    public Text centreDisplay;
    public Image HUD;
    public Image crosshair;
    private GameObject[] Squad = new GameObject[100];
    private string view_state = "";


    // Use this for initialization
    void Start () {
        //INVESTIGATION font
        centreDisplay.enabled = false;
        HUD.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            displayHUD();
        }
        
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StateMachine.Instance.memberState = MemberState.FollowLeader;
            StartCoroutine(ShowMessage("Follow Leader!", 1.0f));
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            StateMachine.Instance.memberState = MemberState.FindCover;
            StartCoroutine(ShowMessage("Find Cover!", 1.0f));
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            StateMachine.Instance.memberState = MemberState.Attack;
            StartCoroutine(ShowMessage("Attack!", 1.0f));
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            StartCoroutine(ShowMessage("Form Line!", 1.0f));
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            StartCoroutine(ShowMessage("Form V!", 1.0f));
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            StartCoroutine(ShowMessage("Follow Player!", 1.0f));
        }
    }


    IEnumerator ShowMessage(string message, float delay)
    {
        centreDisplay.text = message;
        centreDisplay.enabled = true;
        yield return new WaitForSeconds(delay);
        centreDisplay.enabled = false;

    }


    public void SetCrosshairState()
    {
        switch (StateMachine.Instance.viewState)
        {
            case ViewState.Ally:
                
                crosshair.color = Color.blue;
                break;
            case ViewState.Enemy:
                crosshair.color = Color.red;
                break;
            case ViewState.Default:
                crosshair.color = Color.green;
                break;
        }
    }

    void displayHUD()
    {
        if (StateMachine.Instance.viewState == ViewState.Ally)
        {
            crosshair.enabled = false;
            HUD.enabled = true;
            HUD.color = new Vector4(150,250,250,1);
            
        }
    }
}
