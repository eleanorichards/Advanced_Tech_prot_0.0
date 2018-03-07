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
    //public static StateMachine Instance;

    //void Awake()
    //{
    //    Instance = FindObjectOfType<StateMachine>();
    //}

    public GlobalState globalState = GlobalState.Default;
}