using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadPlacement : MonoBehaviour
{

    public GameObject SquadMember;
    private LayerMask ground;
    private float max_distance = 2.0f;
    private float cast_radius = 0.5f;

    private string member_type = "";
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");

        member_type = player.GetComponent<BasicShoot>().current_bullet_type;

        ground = LayerMask.GetMask("Default");
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
