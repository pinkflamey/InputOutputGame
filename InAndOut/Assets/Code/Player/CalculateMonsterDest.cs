using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;

public class CalculateMonsterDest : MonoBehaviour
{
    [SerializeField] private GameObject destMarker;

    [SerializeField] private int heartrate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        heartrate = GameManager.GameInfo.GetHeartRate();
    }

    void CalculateMarkerDistance(int hr)
    {
        
    }
}
