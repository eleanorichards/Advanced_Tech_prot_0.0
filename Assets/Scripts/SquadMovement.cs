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
    private bool following_leader = false;
    private string statename = "";

    public float bump_radius = 5.0f;
    public float immediate_range = 5.0f;

    UnityEngine.AI.NavMeshAgent agent;

    private Detection detection;
    private StateMachine _SM;



    // Use this for initialization
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        detection = GetComponentInChildren<Detection>();
        _SM = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        switch(_SM.memberState)
        {
            case MemberState.Default:
                break;
            case MemberState.FollowLeader:
                FollowLeader();
                break;
            case MemberState.FindCover:
                RunToCover();
                break;
            case MemberState.Attack:
                AttackEnemies();
                break;
            case MemberState.FollowMe:
                FollowPlayer();
                break;
            case MemberState.FormLine:
                FormLine();
                break;
            case MemberState.FormV:
                FormV();
                break;
            default:
                
                break;
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
        agent.SetDestination(detection.LeaderPosition());
    }


    void FormV()
    {
        
    }


    void FormVShape()
    {
    }


    void FormLine()
    {
       
    }

    void FollowPlayer()
    {
        agent.SetDestination(detection.RecallPosition());
    }

    void AttackEnemies()
    {
        agent.SetDestination(detection.ClosestEnemyTransform());
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            //enemy damage script
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("Cover"))
        {
            in_cover = true;
        }

    }


    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Cover"))
        {
            in_cover = false;
        }
    }
}
