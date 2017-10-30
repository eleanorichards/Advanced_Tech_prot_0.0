using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//not doing multiple orders at once is because of temp_distance


public class Detection : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>(100);
    public List<GameObject> allies = new List<GameObject>(100);
    public List<GameObject> cover_points = new List<GameObject>(500);

    public GameObject closest_enemy = null;
    public GameObject closest_cover = null;
    private LayerMask cover_mask = 9;
    public float enemy_distance = Mathf.Infinity;
    public float cover_distance = Mathf.Infinity;
    public float ally_distance = Mathf.Infinity;


    public bool is_leader = false;
    public Vector3 target_pos = Vector3.zero;
    private GameObject leader = null;
    private GameObject player = null;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        //foreach(GameObject cover in cover_points)
        //{
        //    if(!cover.GetComponent<CoverChecker>().isActive())
        //    {
        //        cover.SetActive(false);
        //        print("set inactive: " + cover);
        //    }
        //}
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
                if (curDistance < enemy_distance)
                {
                    closest_enemy = enemy;
                    enemy_distance = curDistance;                   
                }

            }
        }
    }

    public Vector3 ClosestEnemyTransform()
    {
        //if(closest_enemy = null)
        //{
        //    FindNearestEnemy();
        //}
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
    //ENEMY END


    //LEADER START

    public void ChangeAllLeaders(GameObject _leader)
    {
        leader = _leader;
    }


    public void SetLeader(bool setLeader)
    {        
        foreach (GameObject ally in allies)
        {
            ally.GetComponentInChildren<Detection>().ChangeAllLeaders(gameObject);
            if (ally != this.gameObject)
            {
                if (ally.GetComponentInChildren<Detection>().IsLeader())
                {
                    ally.GetComponentInChildren<Detection>().SetLeader(false);
                }
            }
            ally.GetComponent<StateMachine>().memberState = MemberState.FollowLeader;
        }
        is_leader = setLeader;
    }


    public bool IsLeader()
    {
        return is_leader;
    }


    public Vector3 LeaderPosition()
    {
        if (!is_leader)
            return leader.transform.position;
        else
            return transform.position;
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
                    if (curDistance < cover_distance)
                    {
                        closest_cover = cover;
                        cover_distance = curDistance;
						target_pos = closest_cover.transform.position;
                    }
                }
            }
            else
            {
                print("no cover for some reason");
            }
        }
    }


    private bool CoverCanSeeEnemy(GameObject cover_point)
    {
        
        FindNearestEnemy();
        
        bool return_value = true;
        //raycast between cover and closest enemy to player        
        if (cover_point && closest_enemy)
        {
            //foreach(GameObject enemy in enemies)
            //{
                RaycastHit hit;
                Vector3 direction = (cover_point.transform.position - closest_enemy.transform.position).normalized;
                Ray ray = new Ray(cover_point.transform.position, direction);
                if (Physics.Raycast(ray, out hit, cover_mask))
                {
                    Debug.DrawRay(cover_point.transform.position, direction, Color.red);
                    return_value =  true;
                }
                else
                {
                    Debug.DrawRay(cover_point.transform.position, direction, Color.blue);
                    return_value = false;
                }
           // }
        }
        else if(!closest_enemy)
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
            print("returning null");
			return transform.position;
		}
	}
    //COVER END


  

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


    //FOLLOW ME START
    void FollowMe()
    {

    }


    public Vector3 RecallPosition()
    {
        return player.transform.position;
    }
    //FOLLOW ME END


    //ADDITIONS
    void OnTriggerEnter(Collider col)
    {
        //Enemy addition
        if (col.gameObject.CompareTag("Enemy"))
        {
            print("Enemy Added");
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
                print("enemy Removed");
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
