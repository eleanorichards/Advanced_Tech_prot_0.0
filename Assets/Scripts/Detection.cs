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
    private LayerMask cover_mask = 9;
    private float temp_distance = Mathf.Infinity;
    private bool is_leader = false;
    private Vector3 target_pos = Vector3.zero;
    private GameObject leader = null;
    // Use this for initialization
    void Start()
    {
        foreach(GameObject cover in cover_points)
        {
            if(!cover.GetComponent<CoverChecker>().isActive())
            {
                cover.SetActive(false);
                print("set inactive: " + cover);
            }
        }
    }

    //ENEMY START
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
    //ENEMY END


    //LEADER START
    private void FindLeader(/*GameObject selected*/)
    {
        foreach (GameObject ally in allies)
        {
            if (ally.GetComponentInChildren<Detection>().IsLeader())
            {
                leader = ally;
            }
            else
                return;
        }
    }

    public void FollowLeader()
    {
        if(leader == null)
        {
            FindLeader();
        }
        else
        {
            foreach (GameObject ally in allies)
            {
                Vector3 distance = leader.transform.position - transform.position;
                float curDistance = distance.sqrMagnitude;
                Debug.Log(curDistance);
                //if (curDistance > bump_radius)
                //{
                target_pos = leader.transform.position;
                //}

            }

        }
    }

    public void SetLeader(bool setLeader)
    {        
        foreach (GameObject ally in allies)
        {
            if (ally != this.gameObject)
            {
                if (ally.GetComponentInChildren<Detection>().IsLeader())
                {
                    ally.GetComponent<Detection>().SetLeader(false);
                }
            }
        }
        is_leader = setLeader;
        Debug.Log("set a leader");
    }

    public bool IsLeader()
    {
        return is_leader;
    }


    public Vector3 LeaderPosition()
    {
        return target_pos;
    }
    //LEADER END

    //COVER START
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
						target_pos = closest_cover.transform.position;
                    }
                }
            }
        }
    }


    private bool CoverCanSeeEnemy(GameObject cover_point)
    {
        if(!closest_enemy)
        {
            FindNearestEnemy();
        }
        bool return_value = true;
        //raycast between cover and closest enemy to player        
        if (cover_point)
        {
            foreach(GameObject enemy in enemies)
            {
                RaycastHit hit;
                Vector3 direction = (cover_point.transform.position - closest_enemy.transform.position).normalized;
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

	public Vector3 ClosestCoverTransform()
	{
		FindNearestCover ();
		if (target_pos != Vector3.zero)
        {
			return target_pos;
		}
        else
        {
			return transform.position;
		}
	}
    //COVER END


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

    //FORM LINE START
    void FormLine()
    {
        foreach (GameObject ally in allies)
        {
            if (is_leader)
            {
                return;
            }
            else if(ally.GetComponent<Detection>().IsLeader())
            {
               // ally.transform
               // Vector3 offset = temp_loc + i * multiplier;
                //i++
            }
        }
    }
    //FORM LINE END


        //ADDITIONS
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
