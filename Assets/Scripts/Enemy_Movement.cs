using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Movement : MonoBehaviour {

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

	//Movement Stuff
	public float terminal_time = 5.0f;
	private float target_time;
	private int patrolPoint = 0;
	private int previous_position;
	private Vector3 target = Vector3.zero;
	private bool timer_active = false;
	private float current_time = 0.0f;
	private bool move_target = false;
	private float timer = 0.0f;
    private GameObject FOV;

    //FOV
    private Vector3 dist = new Vector3();
    private Vector3 distprevframe = new Vector3();
    private Vector3 dir = new Vector3();

    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
		enemyState = EnemyState.Idle;
		int i = Random.Range (0, patrolPoints.Count);
		previous_position = i;
		target = patrolPoints [i].transform.position;
		target_time	= terminal_time;
        FOV = GetComponentInChildren<FOV>().gameObject;
    }


    void Update()
    {
        Patrol();
    }

	void Patrol()
	{
		if (timer_active) 
		{
			current_time += Time.deltaTime;
			if (current_time >= terminal_time) 
			{
				move_target = true;
				timer_active = false;
				current_time = 0f;
			}
			terminal_time = target_time;
		}
		if (move_target) 
		{
			
			SetTarget ();
			move_target = false;
		}
		Move();
        agent.SetDestination(target);
	}

	void SetTarget()
	{
		int i = Random.Range(0, patrolPoints.Count);
		if (previous_position == i)
		{
			i = Random.Range(0, patrolPoints.Count);
		}
		target = patrolPoints[i].transform.position;
		previous_position = i;
	}

	private void Move()
	{
		timer += Time.deltaTime;
		if (timer >= 0.05f) 
		{
			//FOVRotation();
			timer = 0;
		}
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == patrolPoints[previous_position])
		{
			timer_active = true;

		}
	}

    void FOVRotation()
    {

        dist = FOV.transform.position;
        dir = dist - distprevframe;
        dir = dir * 90;
        distprevframe = FOV.transform.position;

        float angle = Mathf.Atan2(dir.z, dir.y) * Mathf.Rad2Deg;
        FOV.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

}
