using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Radar : MonoBehaviour
{
    [SerializeField] private float distanceToMonster;
    [SerializeField] private int distanceLevel = 0;
    
    [Header("Distance levels")]
    
    [SerializeField] private float[] level0 = {/*Above*/ 50, /*Under*/ Mathf.Infinity, /*Level*/ 0};
    [SerializeField] private float[] level1 = {/*Above*/ 25, /*Under*/ 50, /*Level*/ 1};
    [SerializeField] private float[] level2 = {/*Above*/ 15, /*Under*/ 25, /*Level*/ 2};
    [SerializeField] private float[] level3 = {/*Above*/ 10, /*Under*/ 15, /*Level*/ 3};
    [SerializeField] private float[] level4 = {/*Above*/ 0, /*Under*/ 10, /*Level*/ 4};
    private float[][] levels;

    [SerializeField] private GameObject monster;
    [SerializeField] private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        monster = GameObject.Find("Monster");
        
        levels = new float[][]{level0, level1, level2, level3, level4};

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToMonster = CalculateDistance();

        distanceLevel = (int)CalculateLevel(distanceToMonster);
    }

    private float CalculateLevel(float distance)
    {
        foreach (float[] level in levels) //For each possible level;
        {
            if (distance > level[0] && distance < level[1]) //If the distance is above [0] and under [1];
            {
                return level[2]; //Return the level ([3])
            }
        }

        return -1; //Else, return -1

    }

    private float CalculateDistance()
    {
        NavMeshPath path = new NavMeshPath(); //Create abstract path
        
        //Calculate path between player and monster and store in path
        NavMesh.CalculatePath(
            transform.position, monster.transform.position, NavMesh.AllAreas, path
            );

        Vector3[] points = new Vector3[path.corners.Length + 2];
        
        points[0] = transform.position; //First point is player position
        points[points.Length - 1] = monster.transform.position; //Last point is monster position
        
        // The points in between are the corners of the path.
        for(int i = 0; i < path.corners.Length; i++)
        {
            points[i + 1] = path.corners[i];
        }
        
        float totalDistance = 0; //Setup distance variable
        
        for(int i = 0; i < points.Length - 1; i++)
        {
            totalDistance += Vector3.Distance(points[i], points[i + 1]);
        }
        
        

        return totalDistance;
    }
    
    
}
