using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSquadMovement : MonoBehaviour
{
    private GlobalStateMachine _GSM;
    public float distance_apart = 4.0f;
    private List<GameObject> squaddies = new List<GameObject>();
    public bool stateChanged = false;
    private GameObject player;
    // Use this for initialization
    void Start()
    {
        _GSM = GameObject.Find("GlobalStateMachine").GetComponent<GlobalStateMachine>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (_GSM.globalState)
        {
            case GlobalState.Default:
                //LEAVE GROUP //SET ALL COLOURS TO BLUE OR SOMETHING
                break;

            case GlobalState.Attack:
                FillSquadList();
                foreach (GameObject squaddie in squaddies)
                    squaddie.GetComponent<StateMachine>().memberState = MemberState.Attack;
                break;

            case GlobalState.FindCover:
                foreach (GameObject squaddie in squaddies)
                    squaddie.GetComponent<SquadMovement>().RunToCover();
                break;

            case GlobalState.FormV:
                if (!stateChanged)
                    FormV();
                break;

            case GlobalState.FormLine:
                if (!stateChanged)
                    FormLine();
                break;

            case GlobalState.FollowMe:
                foreach (GameObject squaddie in squaddies)
                    squaddie.GetComponent<SquadMovement>().SetTargetPos(player.transform.position);
                break;

            default:
                break;
        }
    }

    private void FillSquadList()
    {
        foreach (GameObject squaddie in GameObject.FindGameObjectsWithTag("Ally"))
        {
            squaddies.Add(squaddie);
        }
    }

    private void FormV()
    {
        stateChanged = true;
        squaddies.Clear();

        FillSquadList();

        Vector3 leaderPos = squaddies[0].transform.position;

        Vector3 target = Vector3.zero;
        Vector3 pos_change = Vector3.zero;
        bool toggle = true;
        int change_mult = 1;

        bool two_at_front = false;

        two_at_front = ((squaddies.Count - 1) % 2) == 0;

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
            if (squaddie != squaddies[0])
                squaddie.GetComponent<SquadMovement>().SetTargetPos(leaderPos + pos_change);
        }
    }

    private void FormLine()
    {
        stateChanged = true;
        //detection = squaddie.GetComponentInChildren<Detection>();
        int multiplier = 1;
        float offset_change = 2f;
        Vector3 temp_offset = squaddies[0].transform.position;

        foreach (GameObject squaddie in squaddies)
        {
            temp_offset += new Vector3(offset_change * multiplier, 0f, 0f);
            multiplier++;
            print(multiplier);
            squaddie.GetComponent<SquadMovement>().SetTargetPos(temp_offset);        //agent.SetDestination(detection.FormLineTransform());
        }
    }
}