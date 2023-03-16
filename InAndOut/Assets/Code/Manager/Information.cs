using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour
{
    public int heartrate;

    private ReadTextFile rtf;

    // Start is called before the first frame update
    void Start()
    {
        rtf = GetComponent<ReadTextFile>();
    }

    // Update is called once per frame
    void Update()
    {
        //Store heartrate in information script, not only in ReadTextFile
        Int32.TryParse(rtf.GetFileData(), out heartrate);
    }
}
