using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MemberState
{
    Default,
    Attack,
    FindCover,
    FollowLeader,
    FormV,
    FormLine,
    FollowMe
}



public class StateMachine : MonoBehaviour
{
    //public static StateMachine Instance;

    //void Awake()
    //{
    //    Instance = FindObjectOfType<StateMachine>();        
    //}

    public MemberState memberState = MemberState.Default;
}
