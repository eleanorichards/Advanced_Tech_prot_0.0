using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public GameObject HUD;
    public GameObject pubHUD;
    public Image crosshair;
    private string view_state = "";
    private GameObject player;

    // Use this for initialization
    private void Start()
    {
        //INVESTIGATION font
        player = GameObject.Find("Player");
        HUD.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        //Switch states depending on keycodes..
        //if(Input.GetKey(KeyCode.Q))
        //{
        //    displayHUD();
        //}
    }

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

    public void displayMemberHUD()
    {
        if (ViewStateMachine.Instance.viewState == ViewState.Ally)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<BasicShoot>().enabled = false;
            crosshair.enabled = false;
            HUD.SetActive(true);
        }
    }

    public void displayGlobalHUD()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<BasicShoot>().enabled = false;
        crosshair.enabled = false;
        pubHUD.SetActive(true);
    }

    private void CloseHud()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<BasicShoot>().enabled = true;
        crosshair.enabled = true;
        HUD.SetActive(false);
    }
}