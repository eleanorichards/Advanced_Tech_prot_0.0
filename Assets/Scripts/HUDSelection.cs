using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDSelection : MonoBehaviour
{

    private GameObject player;
    public Image crosshair;
    private GameObject ally_selected = null;

    // Use this for initialization
    void Start()
    {
       // ally_selected.GetComponent<StateMachine>();

        player = GameObject.Find("Player");
    }

    public void RecieveButtonmessage(string message)
    {
        switch (message)
        {
            case "Follow":
                ally_selected.GetComponentInChildren<Detection>().SetLeader(true);
                ally_selected.GetComponent<StateMachine>().memberState = MemberState.FollowLeader;
                break;
            case "Attack":
                ally_selected.GetComponent<StateMachine>().memberState = MemberState.Attack;
                break;
            case "Cover":
                ally_selected.GetComponent<StateMachine>().memberState = MemberState.FindCover;
                break;
            case "Wedge":
                ally_selected.GetComponent<StateMachine>().memberState = MemberState.FormV;
                break;
            case "Line":
                ally_selected.GetComponent<StateMachine>().memberState = MemberState.FormLine;
                break;
            case "Recall":
                ally_selected.GetComponent<StateMachine>().memberState = MemberState.FollowMe;
                break;
            case "Exit":
                CloseHud();
                break;
        }
        CloseHud();

    }

    public void SetSelected(GameObject _ally)
    {
        ally_selected = _ally;
    }


    void CloseHud()
    {
        gameObject.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<BasicShoot>().enabled = true;
        crosshair.enabled = true;
    }
}