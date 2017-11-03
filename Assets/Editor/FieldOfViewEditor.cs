using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FOV))]
public class FieldOfViewEditor : Editor
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSceneGUI()
    {
        FOV fow = (FOV)target;
        //Draws view reach
        Handles.color = Color.yellow;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.view_radius);

        //Draws cone of view
        Vector3 viewAngleA = fow.DirFromAngle(-fow.view_angle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.view_angle / 2, false);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.view_radius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.view_radius);

        Handles.color = Color.red;
        foreach(Collider ally in fow.alliesInRadius)
        {
            Handles.DrawLine(fow.transform.position, ally.transform.position);
        }
    }

}