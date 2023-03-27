using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Shapes2D;
using UnityEngine;
using UnityEngine.AI;

public class CalculateMonsterDest : MonoBehaviour
{
    [SerializeField] private GameObject destMarker;

    [Header("Information")]
    
    [SerializeField] [Tooltip("Live heart rate")] private float heartrate; //Live heart rate
    [SerializeField] [Tooltip("Normal heart rate")] private float nHr; //Normal heart rate
    [SerializeField] private float growthFactor = 0.97f; //Growth factor for exponential formula
    [SerializeField] private float multiplierHigher; //heartrate = nHr * multiplierHigher
    [SerializeField] private float calculatedDistance; //The calculated distance based on multiplier

    private GameObject player;
    private NavMeshAgent pAgent;

    private bool canCalc = true;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject;
        pAgent = player.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        heartrate = GameManager.GameInfo.GetHeartRate();
        
        
        /* Maths */
        
        // How much higher/lower is current heart rate than normal?
        multiplierHigher = (heartrate / nHr);

        // Formula for calculating the distance: a = normalHeartrate * 0.966^(normalHeartrate*percentage)
        // So when normalHeartrate = 70, calculating the distance when heartrate is 25% higher than normal:
        // a = 70 * 0,966^(70*1.25)
        // a = 70 * 0.048
        // a = 3.3

        calculatedDistance = CalculateDistance(nHr, growthFactor, multiplierHigher);

        if (canCalc)
        {
            canCalc = false;
            StartCoroutine(Loop());
        }
    }

    private IEnumerator Loop()
    {
        SetPosition(calculatedDistance);
        yield return new WaitForSeconds(1f);
        canCalc = true;
    }

    private float CalculateDistance(float b, float g, float m)
    {
        //a = normal heartrate * 0.966^(normal heartrate * multiplier)
        return b * Mathf.Pow(g, b * m);
    }

    private void SetPosition(float distance)
    {
        bool foundPoint = false;

        while (!foundPoint)
        {
            Vector2 _randDir = Random.insideUnitCircle * distance; //Generate random direction
            Vector3 randDir = new Vector3(_randDir.x, 0, _randDir.y); //Convert to Vector3

            //If the distance between the random point and the player is over 5
            if (Radar.CalculatePathDistance(player.transform.position, randDir) > distance)
            {
                transform.position = randDir;
                foundPoint = true;
            }
        }
    }
    
    
    

}
