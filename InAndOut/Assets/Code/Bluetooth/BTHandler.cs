using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BTHandler : MonoBehaviour
{
    
    [Header("Settings")]
    
    public Color selectColor = Color.red;
    
    public GameObject buttonPrefab;

    public GameObject buttonsParent;

    public GameObject selectTextObj;
    
    [Space]
    
    [Header("Info")]
    public string selectedDeviceName;

    public BluetoothDeviceInfo selectedDevice;
    
    
    //Private variables

    private IEnumerable<BluetoothDeviceInfo> devices;

    private BluetoothClient btClient;

    // Start is called before the first frame update
    void Start()
    {
        btClient = new BluetoothClient();

        devices = GetPairedDevices();
        
        GenerateButtons(devices);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedDevice != null)
        {
            selectedDeviceName = selectedDevice.DeviceName;
        }
    }

    IEnumerable<BluetoothDeviceInfo> GetPairedDevices()
    {
        return btClient.PairedDevices;
    }

    void GenerateButtons(IEnumerable<BluetoothDeviceInfo> devices)
    {
        int i = 0;
        
        foreach (BluetoothDeviceInfo device in devices)
        {
            GameObject button = GameObject.Instantiate(buttonPrefab, buttonsParent.transform);
            /*RectTransform rt = button.GetComponent<RectTransform>();

            rt.position = new Vector3(0, i * 50, 0);*/

            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = device.DeviceName;

            button.GetComponent<DeviceButton>().thisDevice = device;
            

            i++;
        }
    }

    public void SelectDevice(BluetoothDeviceInfo device)
    {
        selectedDevice = device;

        try
        {
            btClient.Connect(selectedDevice.DeviceAddress, BluetoothService.HealthDevice);
        }
        catch
        {
            Debug.Log("Couldn't connect to this device, or it is not a health device!");
        }
        
        selectTextObj.GetComponent<TextMeshProUGUI>().text = selectedDevice.DeviceName;
    }
}
