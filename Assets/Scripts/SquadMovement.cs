using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMovement : MonoBehaviour
{
    private GameObject closest_enemy = null;
    private GameObject closest_cover = null;

    private List<Collider> hitColliders = new List<Collider>(200);
    private List<GameObject> allies = new List<GameObject>(100);
    private float distance = Mathf.Infinity;

    private bool in_cover = false;
    private bool is_leader = false;
    private bool following_leader = false;
    private string statename = "";

    public float bump_radius = 5.0f;
    public float immediate_range = 5.0f;

    UnityEngine.AI.NavMeshAgent agent;

    private Detection detection;


    // Use this for initialization
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        detection = GetComponentInChildren<Detection>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (following_leader)
        {
            FollowLeader();
        }
        if(!following_leader || is_leader)
        {                  
            if (statename == "Attack")
            {
                AttackEnemies();
            }
        }
		if (statename == "Find Cover")
		{            
			RunToCover();
		}
        if (statename == "Find Cover")
        {
            RunToCover();
        }
        if (statename == "Find Cover")
        {
            RunToCover();
        }
    }

    public void ActivateState(string _statename)
    {
        statename = _statename;

        if (statename == "Follow Leader")
        {
            if(following_leader)
            {
                following_leader = false;
            }
            else if(!following_leader)
            {
                following_leader = true;
            }
        }
    }


    void RunToCover()
    {
        if (!in_cover)
        {
            agent.SetDestination(detection.ClosestCoverTransform());
        }
        else
        {
            Debug.Log("In Cover");
        }
    }


    void FollowLeader()
    {
        GetAlliesInRange();
        foreach (GameObject ally in allies)
        {
            if (ally.GetComponent<SquadMovement>().GetLeader() && ally != this.gameObject)
            {
                Vector3 distance = ally.transform.position - transform.position;
                float curDistance = distance.sqrMagnitude;
                if (curDistance > bump_radius)
                {
                    agent.SetDestination(ally.transform.position);
                }
            }

        }
    }


    void FormVShape()
    {
        GetAlliesInRange();
    }


    void FormLine()
    {
        GetAlliesInRange();
        foreach (GameObject ally in allies)
        {
            if (is_leader)
            {
                //Vector3 temp_loc = transform.position;
            }
            else
            {
               // Vector3 offset = temp_loc + i * multiplier;
               //i++
            }
        }
    }


    void AttackEnemies()
    {
        agent.SetDestination(detection.ClosestEnemyTransform());
    }


    void MoveTargetToPosition(Vector3 destination)
    {
        print("set destination " + destination);
        agent.SetDestination(destination);
    }


    public void SetLeader(bool setLeader)
    {
        GetAlliesInRange();
        foreach (GameObject ally in allies)
        {
            if (ally != this.gameObject)
            {
                if (ally.GetComponent<SquadMovement>().GetLeader())
                {
                    ally.GetComponent<SquadMovement>().SetLeader(false);
                }
            }
        }
        is_leader = setLeader;
        Debug.Log("set a leader");
    }


    public bool GetLeader()
    {
        return is_leader;
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //enemy damage script
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Cover")
        {
            in_cover = true;
        }

    }


    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Cover")
        {
            in_cover = false;
        }
    }


    void GetAlliesInRange()
    {
        allies = detection.allies;
        if(allies.Count <= 0)
        {
            print("No allies in range");
        }
    }


}
