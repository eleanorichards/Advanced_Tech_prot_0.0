using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShoot : MonoBehaviour {

    public GameObject bullet;
    public GameObject gun;

    public float force_magnitude = 10.0f;
    private float view_distance = 50.0f;
    private string prev_statename = "Default";

    private Rigidbody bullet_rig;
    private GameObject player = null;
    private GameObject HUD = null;
    private LayerMask cover_mask = 9;
    private Renderer rend = null;
    
    // Use this for initialization
    void Start () {
        bullet_rig = bullet.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        HUD = GameObject.Find("HUD");
	}

    // Update is called once per frame
    void Update()
    {
        ShowCrosshair();
    }


    private void ShowCrosshair()
    {
        RaycastHit hit;
        Debug.DrawRay(gun.transform.position, gun.transform.forward * view_distance, Color.blue);

        if (Physics.Raycast(gun.transform.position, gun.transform.forward, out hit, view_distance, cover_mask))
        {
            if(hit.collider.gameObject.CompareTag("Ally"))
            {
                switchCrosshairState(ViewState.Ally);
                if (Input.GetButtonDown("Fire1")) 
                {
                    AllySelected(hit.collider.gameObject);
                    player.GetComponent<CanvasScript>().displayHUD();               
                }              
            }
            else if(hit.collider.gameObject.tag == "Enemy")
            {
                switchCrosshairState(ViewState.Enemy);
            }
            else
            {
                switchCrosshairState(ViewState.Default);
            }
        }
        if (Input.GetButtonDown("Fire2")) 
        {
            fireGun();
        }
    }


    void fireGun()
    {
        Rigidbody bullet_clone = Instantiate(bullet_rig, gun.transform.position, transform.rotation);
        bullet_clone.velocity = gun.transform.forward * force_magnitude;
    }

    void AllySelected(GameObject ally)
    {
        if (HUD)
        {
            HUD.GetComponent<HUDSelection>().SetSelected(ally);

            rend = ally.GetComponent<Renderer>();
            rend.material.color = Color.red;
        }
        else
            return;
        
    }


    void switchCrosshairState(ViewState state)
    {
        ViewStateMachine.Instance.viewState = state;
        player.GetComponent<CanvasScript>().SetCrosshairState();
    }

}
