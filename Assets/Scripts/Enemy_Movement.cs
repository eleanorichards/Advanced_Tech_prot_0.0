using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Movement : MonoBehaviour
{
    public List<GameObject> patrolPoints = new List<GameObject>(); //Add in inspector

    public enum EnemyState
    {
        Idle,
        Moving,
        Attack
    }

    public EnemyState enemyState;

    private GameObject player;
    private NavMeshAgent agent;

    private bool in_progress = false;
    private int previous_position;
    private Vector3 target = Vector3.zero;
    private GameObject FOV;
    private System.Random _rnd = new System.Random();

    //FOV
    private Vector3 dist;

    private Vector3 distprevframe;
    private Vector3 dir;

    private void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        enemyState = EnemyState.Idle;
        agent.SetDestination(SetTarget());
        FOV = GetComponentInChildren<FOV>().gameObject;
    }

    private void FixedUpdate()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                if (!in_progress)
                    Patrol();

                break;

            case EnemyState.Moving:
                break;

            case EnemyState.Attack:
                break;

            default:
                break;
        }
    }

    private void Patrol()
    {
        in_progress = true;
        float waitTime = _rnd.Next(4, 10);
        StartCoroutine(WaitAtTerminal(waitTime));
    }

    private IEnumerator WaitAtTerminal(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        enemyState = EnemyState.Moving;
        agent.SetDestination(SetTarget());
    }

    private Vector3 SetTarget()
    {
        int i = Random.Range(0, patrolPoints.Count);
        if (previous_position == i)
        {
            i = Random.Range(0, patrolPoints.Count);
        }
        previous_position = i;
        return patrolPoints[i].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PatrolPoint"))
        {
            print("reached point");
            in_progress = false;
            enemyState = EnemyState.Idle;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PatrolPoint"))
        {
            enemyState = EnemyState.Moving;
        }
    }

    private void FOVRotation()
    {
        dist = FOV.transform.position;
        dir = dist - distprevframe;
        dir = dir * 90;
        distprevframe = FOV.transform.position;

        float angle = Mathf.Atan2(dir.z, dir.y) * Mathf.Rad2Deg;
        FOV.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}