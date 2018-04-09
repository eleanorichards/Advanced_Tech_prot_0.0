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

    public float bump_radius = 3.0f;
    public float immediate_range = 5.0f;

    private UnityEngine.AI.NavMeshAgent agent;

    private float distance_apart = 2.0f;
    private Detection detection;
    private StateMachine _SM;
    private GlobalStateMachine _GSM;

    private List<GameObject> squaddies = new List<GameObject>();

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
        switch (_GSM.globalState)
        {
            case GlobalState.Default:
                //LEAVE GROUP //SET ALL COLOURS TO BLUE OR SOMETHING
                break;

            case GlobalState.Attack:
                _SM.memberState = MemberState.Attack;
                break;

            case GlobalState.FindCover:
                _SM.memberState = MemberState.FindCover;
                break;

            case GlobalState.FormV:
                _SM.memberState = MemberState.FormV;
                break;

            case GlobalState.FormLine:
                _SM.memberState = MemberState.FormLine;
                break;

            case GlobalState.FollowMe:
                _SM.memberState = MemberState.FollowMe;
                break;

            default:
                break;
        }
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
        squaddies.Clear();
        foreach (GameObject squaddie in GameObject.FindGameObjectsWithTag("Ally"))
        {
            squaddies.Add(squaddie);
        }
    }

    private void FormVShape()
    {
        squaddies.Clear();
        foreach (GameObject squaddie in GameObject.FindGameObjectsWithTag("Ally"))
        {
            squaddies.Add(squaddie);
        }
        Vector3 leaderPos = squaddies[0].transform.position;
        for (int i = 1; i < squaddies.Count; i++)
        {
            if (i < squaddies.Count / 2)
            {
            }
            else
            {
            }
        }

        //Kris' code
        Vector3 target = Vector3.zero;
        Vector3 pos_change = Vector3.zero;
        bool toggle = true;
        int change_mult = 1;

        bool two_at_front = false;

        //ignored hosted AI

        two_at_front = ((squaddies.Count - 1) % 2) == 0;

        two_at_front = (squaddies.Count % 2) == 0;

        foreach (GameObject squaddie in squaddies)
        {
            if (toggle) //lhs
            {
                pos_change = new Vector3(-change_mult * distance_apart, 0f, change_mult * distance_apart);

                toggle = false;
            }
            else //rhs
            {
                pos_change = new Vector3(change_mult * distance_apart, 0f, change_mult * distance_apart);

                toggle = true;
                change_mult++;
            }
        }
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