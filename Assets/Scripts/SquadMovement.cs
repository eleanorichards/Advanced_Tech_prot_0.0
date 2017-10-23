using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMovement : MonoBehaviour
{

    private GameObject[] enemies = new GameObject[100];
    private GameObject[] allies = new GameObject[100];
    private GameObject[] cover_zones = new GameObject[100];

    //private Collider[] hitColliders;
    private List<Collider> hitColliders = new List<Collider>(200);

    //private List<GameObject> enemies;
    private GameObject closest_enemy;
    private GameObject closest_cover;

    private float distance = Mathf.Infinity;

    private int ally_num = 0;
    private int enemy_num = 0;
    private int cover_num = 0;

    private bool in_cover = false;
    private bool is_leader = false;
    private bool following_leader = false;

    private string statename = "";
    public float search_radius = 10.0f;
    public float bump_radius = 1.0f;
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
        if (statename == "Find Cover")
        {
            RunToCover();
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
        for (int i = 0; i < allies.Length; i++)
        {
            if (allies[i].GetComponent<SquadMovement>().GetLeader() && allies[i] != this.gameObject)
            {
                Vector3 distance = allies[i].transform.position - transform.position;
                float curDistance = distance.sqrMagnitude;
                if (curDistance > bump_radius)
                {
                    Debug.Log(curDistance);
                    agent.SetDestination(allies[i].transform.position);
                }
            }

        }
    }

    void AttackEnemies()
    {
        Vector3 temp_loc;
        temp_loc = detection.ClosestEnemyTransform();
        Debug.Log(temp_loc);
        agent.SetDestination(temp_loc);

    }


    public void SetLeader(bool setLeader)
    {
        for (int i = 0; i < allies.Length; i++)
        {
            if (allies[i] != this.gameObject)
            {
                if (allies[i].gameObject.GetComponent<SquadMovement>().GetLeader())
                {
                    allies[i].gameObject.GetComponent<SquadMovement>().SetLeader(false);
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


}
