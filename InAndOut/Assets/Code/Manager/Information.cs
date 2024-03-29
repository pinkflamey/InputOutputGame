using System;
using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using TMPro;
using UnityEngine;

public class Information : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private int heartrate;
    [SerializeField] private int nHr;

    //Private internal variables
    private ReadTextFile rtf;
    private TextMeshProUGUI debug_hr_ui;

    // Start is called before the first frame update
    void Start()
    {
        rtf = GetComponent<ReadTextFile>();
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            debug_hr_ui = GameObject.Find("HR-debug").GetComponent<TextMeshProUGUI>();
            //Set debug text
            debug_hr_ui.text = heartrate.ToString();
        }
        catch{}
        
        //Store heartrate in information script, not only in ReadTextFile
        Int32.TryParse(rtf.GetFileData(), out heartrate);
        
        
    }

    public GameObject GetDebugObject(Debugger.DebugTools tool)
    {
        if (tool == Debugger.DebugTools.HR_text)
        {
            try
            {
                return debug_hr_ui.gameObject;
            }
            catch{}
        }

        return null;
    }

    public int GetHeartRate()
    {
        return heartrate;
    }

    public int GetNHr()
    {
        return nHr;
    }

    public void SetNHr(int nHr)
    {
        this.nHr = nHr;
    }
}
