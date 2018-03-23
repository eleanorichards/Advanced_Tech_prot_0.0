using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDSelection : MonoBehaviour
{
    private GameObject player;
    public Image crosshair;
    private GameObject ally_selected = null;
    private GlobalStateMachine _GS;

    // Use this for initialization
    private void Start()
    {
        player = GameObject.Find("Player");
        _GS = GameObject.Find("GlobalStateMachine").GetComponent<GlobalStateMachine>();
    }

    public void RecieveButtonmessage(string message)
    {
        if (ally_selected)
            ally_selected.GetComponentInChildren<Detection>().SetLeader(true);
        switch (message)
        {
            case "Follow":
                ally_selected.GetComponentInChildren<Detection>().SetAllToFollowState();
                ally_selected.GetComponent<StateMachine>().memberState = MemberState.FollowLeader;
                break;

            case "Attack":
                ally_selected.GetComponent<StateMachine>().memberState = MemberState.Attack;
                break;

            case "Cover":
                ally_selected.GetComponent<StateMachine>().memberState = MemberState.FindCover;
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

    public void RecieveGlobalButtonmessage(string message)
    {
        switch (message)
        {
            case "Attack":
                _GS.globalState = GlobalState.Attack;
                break;

            case "Cover":
                _GS.globalState = GlobalState.FindCover;
                break;

            case "Wedge":
                _GS.globalState = GlobalState.FormV;
                break;

            case "Line":
                _GS.globalState = GlobalState.FormLine;
                break;

            case "Recall":
                _GS.globalState = GlobalState.FollowMe;
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

    private void CloseHud()
    {
        gameObject.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<BasicShoot>().enabled = true;
        crosshair.enabled = true;
    }
}