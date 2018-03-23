using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadPlacement : MonoBehaviour
{
    public GameObject SquadMember;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            Instantiate(SquadMember, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}