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

    public float bump_radius = 3.0f;
    public float immediate_range = 5.0f;

    private UnityEngine.AI.NavMeshAgent agent;

    private Detection detection;
    private StateMachine _SM;
    private GlobalStateMachine _GSM;

    // Use this for initialization
    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        detection = GetComponentInChildren<Detection>();
        _SM = GetComponent<StateMachine>();
        _GSM = GameObject.Find("GlobalStateMachine").GetComponent<GlobalStateMachine>();
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

    private void RunToCover()
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

    private void FormV()
    {
    }

    private void FormVShape()
    {
    }

    private void FormLine()
    {
        SetTargetPos(detection.FormLineTransform());
        //agent.SetDestination(detection.FormLineTransform());
    }

    private void FollowPlayer()
    {
        SetTargetPos(detection.RecallPosition());
    }

    private void AttackEnemies()
    {
        SetTargetPos(detection.ClosestEnemyTransform());
    }

    private void SetTargetPos(Vector3 _target)
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