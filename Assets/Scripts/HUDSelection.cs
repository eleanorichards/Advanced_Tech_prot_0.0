using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDSelection : MonoBehaviour
{

    private GameObject player;
    public Image crosshair;

    // Use this for initialization
    void Start()
    {

        player = GameObject.Find("Player");
    }

    public void RecieveButtonmessage(string message)
    {
        switch (message)
        {
            case "Follow":
                StateMachine.Instance.memberState = MemberState.FollowLeader;
                break;
            case "Attack":
                StateMachine.Instance.memberState = MemberState.Attack;
                break;
            case "Cover":
                StateMachine.Instance.memberState = MemberState.FindCover;
                break;
            case "Wedge":
                StateMachine.Instance.memberState = MemberState.FormV;
                break;
            case "Line":
                StateMachine.Instance.memberState = MemberState.FormLine;
                break;
            case "Recall":
                StateMachine.Instance.memberState = MemberState.FollowMe;
                break;
            case "Exit":
                CloseHud();
                break;
        }
        CloseHud();

    }


    void CloseHud()
    {
        gameObject.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<BasicShoot>().enabled = true;
        crosshair.enabled = true;
    }


    //when follow is pressed
    public void Follow()
    {
        StateMachine.Instance.memberState = MemberState.FollowLeader;
    }
}