using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>(100);
    public List<GameObject> allies = new List<GameObject>(100);
    public List<GameObject> cover_points = new List<GameObject>(500);


    //private GameObject[] cover_zones;
    private GameObject closest_enemy = null;
    private GameObject closest_cover = null;

    private float temp_distance = Mathf.Infinity;


    /// <COVER>
    /// start at 0
    /// higher the weighting, less valuable the waypoint
    ///</COVER>


    //not doing multiple orders at once is because of temp_distance

    // Use this for initialization
    void Start()
    {
        //cover_zones = GameObject.FindGameObjectsWithTag("Cover");
        //for(int i = 0; i < cover_zones.Length; i++)
        //{
        //    if(!cover_zones[i].GetComponent<CoverChecker>().isActive())
        //    {
        //        cover_zones[i].SetActive(false);
        //    }
        //}

        foreach(GameObject cover in cover_points)
        {
            if(!cover.GetComponent<CoverChecker>().isActive())
            {
                //cover.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }


    void OnTriggerEnter(Collider col)
    {
        //Enemy addition
        if (col.gameObject.CompareTag("Enemy"))
        {
            enemies.Add(col.gameObject);
        }
        //Ally addition
        if (col.gameObject.CompareTag("Ally"))
        {
            allies.Add(col.gameObject);
        }
        //cover points addition
        if (col.gameObject.CompareTag("Cover"))
        {
            cover_points.Add(col.gameObject);
        }
    }

    //REMOVALS
    void OnTriggerExit(Collider col)
    {
        //Enemy remove
        if (col.gameObject.CompareTag("Enemy"))
        {
            for(int i = enemies.Count - 1; i >= 0; i--)
            {
                    enemies.RemoveAt(i);
            }             
        }

        //Ally remove
        if (col.gameObject.CompareTag("Ally"))
        {
            for (int i = allies.Count - 1; i >= 0; i--)
            {
                allies.RemoveAt(i);
            }
        }

        //cover remove
        if (col.gameObject.CompareTag("Cover"))
        {
            for (int i = cover_points.Count - 1; i >= 0; i--)
            {
                cover_points.RemoveAt(i);
            }
        }

    }


    private void FindNearestEnemy()
    {     
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Vector3 diff = enemy.transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < temp_distance)
                {
                    closest_enemy = enemy;
                    temp_distance = curDistance;
                    Debug.Log(closest_enemy.name);
                }

            }
        }
    }


    private void FindNearestCover()
    {
        foreach (GameObject cover in cover_points)
        {
            if (cover)
            {
                if(!CoverCanSeeEnemy(cover))
                {
                    Vector3 diff = cover.transform.position - transform.position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < temp_distance)
                    {
                        closest_cover = cover;
                        temp_distance = curDistance;
                        Debug.Log(closest_cover.name);
                    }

                }
            }
        }
    }


    private bool CoverCanSeeEnemy(GameObject cover_point)
    {
        if(!closest_enemy)
        {
            FindNearestEnemy();
            //FindNearestCover();
        }
        if (!cover_point)
        {
            FindNearestCover();
        }
        //raycast between cover and enemy        
        if (cover_point)
        {
            RaycastHit hit;
            Vector3 direction = (cover_point.transform.position - closest_enemy.transform.position).normalized;
            Ray ray = new Ray(cover_point.transform.position, direction);
            Debug.DrawRay(cover_point.transform.position, direction, Color.red);
            if (Physics.Raycast(ray, out hit, 30.0f))
            {
                return true;
            }
            else
            {

                print("returning false");
                return false;
            }
        }
        else
        {
            return false;
        }
        

    }

    public Vector3 ClosestEnemyTransform()
    {
        FindNearestEnemy();
        if (closest_enemy)
        {
            Debug.Log(closest_enemy);
            return closest_enemy.transform.position;
        }
        else
        {
            Debug.Log("No enemies in range");
            return transform.position;
        }
    }


    public Vector3 ClosestCoverTransform()
    {
        FindNearestCover();
        if(closest_cover)
        {
            return closest_cover.transform.position;
        }
        else
        {
            return transform.position;
        }
    }
}


//for(int i = 0; i < cover_zones.Length; i++)
//{         
//    if(!CoverCanSeeEnemy(cover_zones[i]))
//    {
//        Vector3 diff = cover_zones[i].transform.position - transform.position;
//        float curDistance = diff.sqrMagnitude;
//        if (curDistance < temp_distance)
//        {
//            closest_cover = cover_zones[i];
//            temp_distance = curDistance;
//        }

//    }    
//}