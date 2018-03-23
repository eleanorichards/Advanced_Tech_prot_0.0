using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShoot : MonoBehaviour
{
    public GameObject bullet;
    public GameObject gun;

    public float force_magnitude = 10.0f;
    private float view_distance = 50.0f;
    private string prev_statename = "Default";

    private Rigidbody bullet_rig;
    private GameObject player = null;
    public GameObject HUD;
    public LayerMask cover_mask;
    private Renderer rend = null;

    // Use this for initialization
    private void Start()
    {
        bullet_rig = bullet.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        //HUD = GameObject.Find("HUD");
    }

    // Update is called once per frame
    private void Update()
    {
        ShowCrosshair();
    }

    private void ShowCrosshair()
    {
        RaycastHit hit;
        Debug.DrawRay(gun.transform.position, gun.transform.forward, Color.blue);

        if (Physics.Raycast(gun.transform.position, gun.transform.forward, out hit, view_distance, cover_mask))
        {
            if (hit.collider.gameObject.CompareTag("Ally"))
            {
                switchCrosshairState(ViewState.Ally);
                if (Input.GetButtonDown("Fire1"))
                {
                    AllySelected(hit.collider.gameObject);
                    player.GetComponent<CanvasScript>().displayMemberHUD();
                }
            }
            else if (hit.collider.gameObject.tag == "Enemy")
            {
                switchCrosshairState(ViewState.Enemy);
            }
        }
        else
        {
            switchCrosshairState(ViewState.Default);
            if (Input.GetButtonDown("Fire1"))
            {
                player.GetComponent<CanvasScript>().displayGlobalHUD();
            }
            if (Input.GetButtonDown("Fire2"))
            {
                fireGun();
            }
        }
    }

    private void fireGun()
    {
        Rigidbody bullet_clone = Instantiate(bullet_rig, gun.transform.position, transform.rotation);
        bullet_clone.velocity = gun.transform.forward * force_magnitude;
    }

    private void AllySelected(GameObject ally)
    {
        if (!HUD)
            HUD = GameObject.Find("HUD");

        HUD.GetComponent<HUDSelection>().SetSelected(ally);

        rend = ally.GetComponent<Renderer>();
        rend.material.color = Color.red;
    }

    private void switchCrosshairState(ViewState state)
    {
        ViewStateMachine.Instance.viewState = state;
        player.GetComponent<CanvasScript>().SetCrosshairState();
    }
}