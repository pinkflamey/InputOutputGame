using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMan : MonoBehaviour
{
    public GameObject serialObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void clickButtonSendMessage(string messageToSend)
    {
        serialObject.GetComponent<SerialController>().SendSerialMessage(messageToSend);
        Debug.Log("Got Click - Sent " + messageToSend);
    }
}
