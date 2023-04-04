using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class nHrSetter : MonoBehaviour
{
    [SerializeField] private int averageHeartrate;
    
    [Space]
    
    [SerializeField] private Slider slider;

    [SerializeField] private int totalTime;

    [Space]
    
    [SerializeField] private int totalBeats;

    [SerializeField] private int t;

    private TextMeshProUGUI tmp;

    void Start()
    {
        tmp = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
        
        gameObject.SetActive(true);
        
        slider.value = 0;
        slider.maxValue = totalTime;
        
        StartCoroutine(nHrTimer(totalTime));
    }

    IEnumerator FinishSequence()
    {
        averageHeartrate = totalBeats / totalTime;
        GameManager.GameInfo.SetNHr(averageHeartrate);

        tmp.text = "Your average normal heart rate is " + averageHeartrate.ToString() + "!";

        yield return new WaitForSeconds(3f);
        
        GameManager.Instance.LoadScene("MainMenu");
    }

    IEnumerator nHrTimer(int totalTime)
    {
        
        //If the totalTime has passed
        if (t > totalTime)
        {
            StartCoroutine(FinishSequence());
            yield break;
        }
        
        //Add the current heart rate to the list of entries
        totalBeats += GameManager.GameInfo.GetHeartRate();

        yield return new WaitForSeconds(1f); //Wait a second
        t++; //Add a second to the total time passed
        slider.value++; //Add 1 second worth of value to the slider

        StartCoroutine(nHrTimer(totalTime));
    }
}
