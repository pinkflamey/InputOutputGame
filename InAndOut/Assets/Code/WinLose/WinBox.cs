using System;
using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;

public class WinBox : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameManager.Instance.LoadScene("Win");
        }
    }

}
