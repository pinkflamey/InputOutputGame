using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    // Start is called before the first frame update
    void Start()
    {
        monster = GameObject.Find("Monster");
        
        levels = new float[][]{level0, level1, level2, level3, level4};
    }

    // Update is called once per frame
    void Update()
    {
        distanceToMonster = Vector3.Distance(monster.transform.position, transform.position);

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
}
