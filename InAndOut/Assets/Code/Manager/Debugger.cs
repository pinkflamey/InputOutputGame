using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [Header("In-game debug controls")]
    
    public KeyCode kc_HRdebug;
    
    [Header("Debug objects")]
    
    public GameObject HRdebug;

    public enum DebugTools
    {
        HR_text
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            HRdebug = GameManager.GameInfo.GetDebugObject(DebugTools.HR_text);
        }
        catch{}

        if (Input.GetKeyDown(kc_HRdebug))
        {
            //(De)activate debug
            SwitchObjectState(HRdebug);
        }
    }

    void SwitchObjectState(GameObject obj)
    {
        if (obj.activeInHierarchy)
        {
            obj.SetActive(false);
        }
        else
        {
            obj.SetActive(true);
        }
    }
}
