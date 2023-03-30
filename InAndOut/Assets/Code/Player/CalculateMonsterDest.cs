using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Shapes2D;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CalculateMonsterDest : MonoBehaviour
{
    [SerializeField] private GameObject destMarker;
    [SerializeField] private float maxFailedTries;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    [Header("Information")]
    
    [SerializeField] [Tooltip("Live heart rate")] private float heartrate; //Live heart rate
    [SerializeField] [Tooltip("Normal heart rate")] private float nHr; //Normal heart rate
    [SerializeField] private float growthFactor = 0.97f; //Growth factor for exponential formula
    [SerializeField] private float multiplierHigher; //heartrate = nHr * multiplierHigher
    [SerializeField] private float calculatedDistance; //The calculated distance based on multiplier
    [SerializeField] private int failedTries = 0;

    private GameObject player;
    private NavMeshAgent pAgent;
    
    private Vector3 newPos = Vector3.zero;
    private float pathDist = 0.0f;
    private Vector3 randDir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject;
        pAgent = player.GetComponent<NavMeshAgent>();
        
        //Get normal heart rate
        nHr = GameManager.GameInfo.GetNHr();
        
        //Start timer
        StartCoroutine(Timer(5f, 10f));
    }

    // Update is called once per frame
    void Update()
    {
        heartrate = GameManager.GameInfo.GetHeartRate(); //Get the live heart rate

        /* Maths */
        
        // How much higher/lower is current heart rate than normal?
        multiplierHigher = (heartrate / nHr);

         /*Formula for calculating the distance: a = normalHeartrate * 0.966^(normalHeartrate*percentage)
         So when normalHeartrate = 70, calculating the distance when heartrate is 25% higher than normal:
         a = 70 * 0,966^(70*1.25)
         a = 70 * 0.048
         a = 3.3*/

        //Calculate the required distance
        calculatedDistance = CalculateDistance(nHr, growthFactor, multiplierHigher);
    }

    private IEnumerator Timer(float delayHR, float delayRN)
    {
        /*
         * This timer sets a new position every delay seconds
         */
        
        Debug.Log("Restarting timer loop...");

        //If the distance calculated is over 10
        if (calculatedDistance > 10)
        {
            transform.position = GetRandomPosition(); //Set to random position
            
            yield return new WaitForSeconds(delayRN); //Wait delay
        }
        else //If the distance is under or equal to 10
        {
            StartCoroutine(SetHRBasedPosition(calculatedDistance)); //Start attempting to set heart rate based position
        
            yield return new WaitForSeconds(delayHR); //Wait delay
        }
        
        
        StartCoroutine(Timer(delayHR, delayRN));
    }

    private Vector3 GetRandomPosition()
    {
        Bounds bounds = GameObject.Find("Floor").GetComponent<MeshRenderer>().bounds;

        float minX = bounds.size.x * -0.5f;
        float minY = bounds.size.y * -0.5f;
        float minZ = bounds.size.z * -0.5f;

        return new Vector3(Random.Range (minX, -minX), Random.Range (minY, -minY), Random.Range (minZ, -minZ));
    }
    
    private IEnumerator SetHRBasedPosition(float distance)
    {
        Debug.Log("Starting pos calculation...");
        
        //Create random direction to attempt
        randDir = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));

        //Possible position is equal to the random direction * distance
        newPos = randDir * distance;
            
        //Calculate the distance between the new possible position and the player, based on the navmesh
        pathDist = Radar.CalculatePathDistance(newPos, player.transform.position);

        //If the calculated distance based on the navmesh is between minDistance% and maxDistance%
        if (pathDist >= distance * minDistance && pathDist <= distance * maxDistance)
        {
            Debug.Log("Succeeded");

            transform.localPosition = newPos; //Set the new position
            
            //Reset failed tries
            failedTries = 0;
            
            yield return null;
        }
        else //If the attempt was unsuccesful
        {
            failedTries++; //Add to the failed tries
            
            yield return new WaitForSeconds(0.01f); //Delay to prevent overflow

            //If the failed tries has NOT exceeded the maximum failed tries
            if (failedTries < maxFailedTries)
            {
                Debug.Log("Position did not meet requirements. Restarting loop...");
                
                //Restart the loop, try again
                StartCoroutine(SetHRBasedPosition(calculatedDistance));
                yield return null;
            }
            else //If the failed tries has exceeded the maximum failed tries
            {
                Debug.Log("Too many failed tries: setting position in front of player based on distance");

                //Set the marker position to in front of the player based on distance
                transform.position =
                    new Vector3(player.transform.position.x, 0, player.transform.position.z + distance);
            
                yield return null;
            }
            
        }

        yield return null;
    }

    private float CalculateDistance(float b, float g, float m)
    {
        //a = normal heartrate * 0.966^(normal heartrate * multiplier)
        return b * Mathf.Pow(g, b * m);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(newPos, Vector3.one);
        
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector3.one);

        try
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(player.transform.position, new Vector3(calculatedDistance, calculatedDistance, calculatedDistance));
        } catch {}
    }
}
