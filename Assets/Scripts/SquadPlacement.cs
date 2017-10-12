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
            Debug.Log("Squaddie placed on ground");
            Instantiate(SquadMember, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }


    //

    //Wall PLACEMENT NOW DEPRECATED AS IT'S USELESS


    //
    //if (col.gameObject.tag == "Wall")
    //{
    //    Debug.Log("wALL HIT");
    //    RaycastHit hit;
    //    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    Vector3 down = transform.TransformDirection(-Vector3.up);

    //    //if (Physics.Raycast(transform.position, down, out hit, max_distance, ground))
    //    if (Physics.SphereCast(transform.position, cast_radius, down, out hit, max_distance, ground))
    //    {
    //        Debug.Log("Squaddie Inserted ;)");
    //        //Vector3 hit_position = transform.InverseTransformPoint(hit.point);
    //        Vector3 Squaddie_pos = hit.point;
    //        //Debug.Log(hit.point);
    //        Instantiate(SquadMember, Squaddie_pos, transform.rotation);
    //    }
    //    Destroy(gameObject);
    //}


}
