using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {


    public GameObject HUD;
    public Image crosshair;
    private string view_state = "";
    private GameObject player;

    // Use this for initialization
    void Start ()
    {
        //INVESTIGATION font
        player = GameObject.Find("Player");
        HUD.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Switch states depending on keycodes..
        //if(Input.GetKey(KeyCode.Q))
        //{
        //    displayHUD();
        //}    
    }


    //IEnumerator ShowMessage(string message, float delay)
    //{
    //    centreDisplay.text = message;
    //    centreDisplay.enabled = true;
    //    yield return new WaitForSeconds(delay);
    //    centreDisplay.enabled = false;

    //}


    public void SetCrosshairState()
    {
        switch (ViewStateMachine.Instance.viewState)
        {
            case ViewState.Ally:          
                crosshair.color = Color.green;
                break;
            case ViewState.Enemy:
                crosshair.color = Color.red;
                break;
            case ViewState.Default:
                crosshair.color = Color.cyan;
                break;
        }
    }


    public void displayHUD()
    {        
        if (ViewStateMachine.Instance.viewState == ViewState.Ally)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<BasicShoot>().enabled = false;
            crosshair.enabled = false;
            HUD.SetActive(true);          
        }
    }


    void CloseHud()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<BasicShoot>().enabled = true;
        crosshair.enabled = true;
        HUD.SetActive(false);
    }
}
