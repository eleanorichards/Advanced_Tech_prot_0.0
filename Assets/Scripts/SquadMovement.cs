using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMovement : MonoBehaviour
{
    private GameObject closest_enemy = null;
    private GameObject closest_cover = null;

    private float distance = Mathf.Infinity;

    private bool in_cover = false;
    private bool following_leader = false;
    private string statename = "";

    public float bump_radius = 2.0f;
    public float immediate_range = 5.0f;

    private UnityEngine.AI.NavMeshAgent agent;

    private float distance_apart = 2.0f;
    private Detection detection;
    private StateMachine _SM;
    private GlobalStateMachine _GSM;
    private GlobalSquadMovement globalMovement;
    private List<GameObject> squaddies = new List<GameObject>();

    // Use this for initialization
    private void Start()
    {
        globalMovement = GameObject.Find("GlobalStateMachine").GetComponent<GlobalSquadMovement>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        detection = GetComponentInChildren<Detection>();
        _SM = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        switch (_SM.memberState)
        {
            case MemberState.Default:
                break;

            case MemberState.FollowLeader:
                FollowLeader();
                break;

            case MemberState.FindCover:
                if (!in_cover)
                    RunToCover();
                break;

            case MemberState.Attack:
                AttackEnemies();
                break;

            case MemberState.FollowMe:
                FollowPlayer();
                break;

            default:

                break;
        }
    }

    public void RunToCover()
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

    private void FollowLeader()
    {
        SetTargetPos(detection.LeaderPosition());
    }

    private void FollowPlayer()
    {
        SetTargetPos(detection.RecallPosition());
    }

    private void AttackEnemies()
    {
        SetTargetPos(detection.ClosestEnemyTransform());
    }

    public void SetTargetPos(Vector3 _target)
    {
        Vector3 diff = _target - transform.position;
        float curDistance = diff.sqrMagnitude;
        if (curDistance > bump_radius)
        {
            agent.SetDestination(_target);
        }
        else
            agent.SetDestination(transform.position);
        //return;
    }

    private void OnCollisionEnter(Collision col)
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

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Cover"))
        {
            in_cover = false;
        }
    }
}