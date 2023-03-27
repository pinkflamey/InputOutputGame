using System.Collections;
using System.Collections.Generic;
using Shapes2D;
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

    private int oldLevel;
    
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
        distanceToMonster = CalculatePathDistance(gameObject.transform.position, monster.transform.position);

        distanceLevel = (int)CalculateLevel(distanceToMonster);

        //If the serial is available
        if (distanceLevel != oldLevel && GameManager.MicroBit.GetSerialStatus() == true)
        {
            //Send the level to the micro bit
            GameManager.MicroBit.WriteString(distanceLevel.ToString());
            oldLevel = distanceLevel;
        }
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

    public static float CalculatePathDistance(Vector3 a, Vector3 b)
    {
        NavMeshPath path = new NavMeshPath(); //Create abstract path
        
        //Calculate path between player and monster and store in path
        NavMesh.CalculatePath(
            a, b, NavMesh.AllAreas, path
            );

        Vector3[] points = new Vector3[path.corners.Length + 2];
        
        points[0] = a; //First point is player position
        points[points.Length - 1] = b; //Last point is monster position
        
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

    public int GetDistanceLevel()
    {
        return distanceLevel;
    }
    
}
