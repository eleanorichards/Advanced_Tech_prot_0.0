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

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //cover_zones = GameObject.FindGameObjectsWithTag("Cover");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InvokeRepeating("UpdateColCheck", 0.0f, 1.0f);

        if (following_leader)
        {
            FollowLeader();
        }
        if(!following_leader || is_leader)
        {
            if (statename == "Find Cover")
            {
                RunToCover();
            }          
            else if (statename == "Attack")
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
    }


    void RunToCover()
    {
        if (!in_cover)
        {
            agent.SetDestination(FindNearestCover());
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
        FindNearestEnemy();
    }


    Vector3 FindNearestEnemy()
    { 
        for (int i = 0; i < enemy_num; i++)
        {
            if(enemies[i])
            {
                Vector3 diff = enemies[i].transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest_enemy = enemies[i];
                    distance = curDistance;
                }
            }
        }
        if(closest_enemy)
        {
            return closest_enemy.transform.position;
        }
        else
        {
            return Vector3.zero;
        }
        
    }


    Vector3 FindNearestCover()
    {
        FindNearestEnemy();
        Vector3 closest_enemy_loc = Vector3.zero;

        if (FindNearestEnemy() != Vector3.zero) 
        {
            closest_enemy_loc = closest_enemy.transform.position;
        }

        for (int i = 0; i < cover_zones.Length; i++)
        {
            Debug.Log(i);
            if(cover_zones[i])
            {

                Debug.Log("Found available Cover");
                Vector3 diff = cover_zones[i].transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest_cover = cover_zones[i];
                    distance = curDistance;
                }
            }
            //if (!Physics.Raycast(cover_zones[i].transform.position, closest_enemy_loc, 30.0f))
            //{
            //}
        }
        
        if(closest_cover)
        {
            return closest_cover.transform.position;
        }
        else
        { return transform.position; }
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


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Cover")
        {
            in_cover = true;
        }
    }


    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Cover")
        {
            in_cover = false;
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //enemy damage script
            Destroy(col.gameObject);
        }
    }


    void UpdateColCheck()
    {        

        foreach (Collider hitCollider in Physics.OverlapSphere(transform.position, immediate_range))
        {
                if (!hitColliders.Contains(hitCollider))
                {
                    hitColliders.Add(hitCollider);
                }
        }

        enemy_num = 0;
        ally_num = 0;
        cover_num = 0;

        int i = 0;
        foreach(Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy " + i + " Added");
                enemies[enemy_num] = hitColliders[i].gameObject;
                enemy_num++;
            }
            else if(hitCollider.gameObject.CompareTag("Ally"))
            {
                Debug.Log("Enemy " + i + " Added");
                allies[ally_num] = hitColliders[i].gameObject;
                ally_num++;
            }
            else if(hitCollider.gameObject.CompareTag("Cover"))
            {
                Debug.Log("Cover " + i + " Added");

                cover_zones[cover_num] = hitCollider.gameObject;
                cover_num++;
            }
            i++;
        }
        Debug.Log("done");
    }
}
