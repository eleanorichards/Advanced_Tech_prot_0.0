using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShoot : MonoBehaviour {

    public GameObject bullet;
    public GameObject gun;

    public float force_magnitude = 5.0f;
    private Rigidbody bullet_rig;

    // Use this for initialization
    void Start () {
        bullet_rig = bullet.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Rigidbody bullet_clone = (Rigidbody)Instantiate(bullet_rig, gun.transform.position, transform.rotation);
            bullet_clone.velocity = gun.transform.forward * force_magnitude;
            Debug.Log(bullet_clone.velocity);
           // bullet_rig.AddForce(gun.transform.forward * force_magnitude, ForceMode.Impulse);
        }


	}
}
