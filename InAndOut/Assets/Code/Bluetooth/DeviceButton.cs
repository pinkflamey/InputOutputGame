using System.Collections;
using System.Collections.Generic;
using InTheHand.Net.Sockets;
using UnityEngine;
using UnityEngine.UIElements;

public class DeviceButton : MonoBehaviour
{
    public BluetoothDeviceInfo thisDevice;

    public string thisDeviceName;

    public BTHandler btHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        btHandler = GameObject.Find("Bluetooth").GetComponent<BTHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        thisDeviceName = thisDevice.DeviceName;
    }

    public void Select()
    {
        btHandler.SelectDevice(thisDevice);
    }
}
