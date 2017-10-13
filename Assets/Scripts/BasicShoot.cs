using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShoot : MonoBehaviour {

    public GameObject bullet;
    public GameObject gun;
    public string current_bullet_type = "";

    public float force_magnitude = 5.0f;

    private Rigidbody bullet_rig;
    private float view_distance = 20.0f;

    Renderer rend = null;
    // Use this for initialization
    void Start () {
        bullet_rig = bullet.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;
        Debug.DrawRay(gun.transform.position, gun.transform.forward * view_distance, Color.blue);

        if (Physics.Raycast(gun.transform.position, gun.transform.forward, out hit, view_distance))
        {
            if(hit.collider.gameObject.tag == "Ally")
            {
                if(Input.GetKey(KeyCode.Q))
                {
                    rend = hit.collider.gameObject.GetComponent<Renderer>();
                    rend.material.color = Color.red;
                    hit.collider.gameObject.GetComponent<SquadMovement>().SetLeader(true);
                }
                rend = hit.collider.gameObject.GetComponent<Renderer>();
                rend.material.color = Color.green;
            }
        }
        if (Input.GetButtonDown("Fire1")) 
        {
            fireGun();
        }

	}


    void fireGun()
    {
        Rigidbody bullet_clone = Instantiate(bullet_rig, gun.transform.position, transform.rotation);
        bullet_clone.velocity = gun.transform.forward * force_magnitude;
    }
}
