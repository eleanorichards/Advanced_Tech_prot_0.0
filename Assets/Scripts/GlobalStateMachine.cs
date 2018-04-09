using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlobalState
{
    Default,
    Attack,
    FindCover,
    FormV,
    FormLine,
    FollowMe
}

public class GlobalStateMachine : MonoBehaviour
{
    public GlobalState globalState = GlobalState.Default;

    public void SetGlobalState(GlobalState _gs)
    {
        GameObject[] squaddies = GameObject.FindGameObjectsWithTag("Squad");
        foreach (GameObject squadMember in squaddies)
        {
            squadMember.GetComponent<StateMachine>().memberState = MemberState.Default; // set individual state to default
        }
        globalState = _gs;
    }
}