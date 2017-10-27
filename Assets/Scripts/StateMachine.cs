using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MemberState
{
    Attack,
    FindCover,
    FollowLeader,
    FormV,
    FormLine,
    FollowMe
}

public enum ViewState
{
    Default,
    Ally,
    Enemy,

}

public class StateMachine : MonoBehaviour
{
    public static StateMachine Instance;

    void Awake()
    {
        Instance = FindObjectOfType<StateMachine>();        
    }

    public MemberState memberState = MemberState.FindCover;
    public ViewState viewState = ViewState.Default;
}
