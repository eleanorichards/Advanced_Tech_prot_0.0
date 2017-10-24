using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadPlacement : MonoBehaviour
{

    public GameObject SquadMember;

    // Use this for initialization
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Instantiate(SquadMember, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
