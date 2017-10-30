using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ViewState
{
    Default,
    Ally,
    Enemy,
}

public class ViewStateMachine : MonoBehaviour
{
    public static ViewStateMachine Instance;

    void Awake()
    {
        Instance = FindObjectOfType<ViewStateMachine>();
    }

    public ViewState viewState = ViewState.Default;
}
