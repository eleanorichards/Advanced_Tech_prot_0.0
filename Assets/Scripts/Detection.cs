using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//not doing multiple orders at once is because of temp_distance


public class Detection : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>(100);
    public List<GameObject> allies = new List<GameObject>(100);
    public List<GameObject> cover_points = new List<GameObject>(500);

    private GameObject closest_enemy = null;
    private GameObject closest_cover = null;
	private Vector3 targetPos;
    private LayerMask cover_mask = 9;
    private float temp_distance = Mathf.Infinity;
    

    // Use this for initialization
    void Start()
    {

		targetPos = Vector3.zero;

        foreach(GameObject cover in cover_points)
        {
            if(!cover.GetComponent<CoverChecker>().isActive())
            {
                //cover.SetActive(false);
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
    }


    private void FindNearestEnemy()
    {     
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Vector3 diff = enemy.transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < temp_distance)
                {
                    closest_enemy = enemy;
                    temp_distance = curDistance;
                }

            }
        }
    }


	void FindNearestCover()
    {
		Vector3 closest_transform = transform.position;
        foreach (GameObject cover in cover_points)
        {
            if (cover)
            {
                if(!CoverCanSeeEnemy(cover))
                {
                    Vector3 diff = cover.transform.position - transform.position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < temp_distance)
                    {
                        closest_cover = cover;
                        temp_distance = curDistance;
						targetPos = closest_cover.transform.position;
                    }
                }
            }
        }
    }


    private bool CoverCanSeeEnemy(GameObject cover_point)
    {
        bool return_value = true;
        //raycast between cover and closest enemy to player        
        if (cover_point)
        {
            foreach(GameObject enemy in enemies)
            {
                RaycastHit hit;
                Vector3 direction = (cover_point.transform.position - enemy.transform.position).normalized;
                Ray ray = new Ray(cover_point.transform.position, direction);
                Debug.DrawRay(cover_point.transform.position, direction, Color.red);
                if (Physics.Raycast(ray, out hit, cover_mask))
                {
                    return_value =  true;
                }
                else
                {
                    return_value = false;
                }
            }
        }
        else
        {
            return_value = false;
        }
        return return_value;
    }

    public Vector3 ClosestEnemyTransform()
    {
        FindNearestEnemy();
        if (closest_enemy)
        {
            return closest_enemy.transform.position;
        }
        else
        {
            Debug.Log("No enemies in range");
            return transform.position;
        }
    }


	public Vector3 ClosestCoverTransform()
	{
		FindNearestCover ();
		if (targetPos != Vector3.zero) {
			return targetPos;
		} else {
			return transform.position;
		}
	}


    void OnTriggerEnter(Collider col)
    {
        //Enemy addition
        if (col.gameObject.CompareTag("Enemy"))
        {
            enemies.Add(col.gameObject);
        }
        //Ally addition
        if (col.gameObject.CompareTag("Ally"))
        {
            allies.Add(col.gameObject);
        }
        //cover points addition
        if (col.gameObject.CompareTag("Cover"))
        {
            cover_points.Add(col.gameObject);
        }
    }

//REMOVALS
void OnTriggerExit(Collider col)
{
    //Enemy remove
    if (col.gameObject.CompareTag("Enemy"))
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            enemies.RemoveAt(i);
        }
    }
    //Ally remove
    if (col.gameObject.CompareTag("Ally"))
    {
        for (int i = allies.Count - 1; i >= 0; i--)
        {
            allies.RemoveAt(i);
        }
    }
    //cover remove
    if (col.gameObject.CompareTag("Cover"))
    {
        for (int i = cover_points.Count - 1; i >= 0; i--)
        {
            cover_points.RemoveAt(i);
        }
    }
}
}
