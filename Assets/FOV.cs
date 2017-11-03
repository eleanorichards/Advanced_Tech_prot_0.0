
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{

    Vector2 ray_direction = Vector2.zero;

    public float view_radius = 5.0f;
    [Range(0, 360)]
    public float view_angle;

    public LayerMask ignoreMask; //Set to spirit and player
    public LayerMask acceptMask; //anything you want to return in the view radius

    public Collider[] alliesInRadius;
    private GameObject parent;
    private Light spot;
    //[HideInInspector]
    public List<GameObject> allies = new List<GameObject>(100);

    // Use this for initialization
    void Start()
    {
        StartCoroutine("FindTargets", 0.2f);
        parent = transform.parent.gameObject;
        spot = GetComponent<Light>();
    }

    IEnumerator FindTargets(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FOVCast();
        }
    }

    private void FOVCast()
    {
        //any spiritsInRadius are in the circle
        allies.Clear();
        alliesInRadius = Physics.OverlapSphere(transform.position, view_radius, acceptMask);

        foreach (Collider ally in alliesInRadius)
        {
            Transform target = ally.transform;
            Vector3 ray_direction = (ally.transform.position - transform.position);
            //find angle between my agent and the hit is it in my field of view
            if (Vector3.Angle(transform.forward, ray_direction) < view_angle / 2)
            {
                //this is inside the view angle
                if (!Physics.Raycast(transform.position, ray_direction, 50.0f, ignoreMask))
                {
                    if (ally.CompareTag("Ally") && !allies.Contains(target.gameObject)
                        && target.gameObject != gameObject.transform.parent)
                    {
                        allies.Add(target.gameObject);
                        spot.color = Color.magenta;
                    }
                }
                else
                    spot.color = Color.white;

            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }




}
