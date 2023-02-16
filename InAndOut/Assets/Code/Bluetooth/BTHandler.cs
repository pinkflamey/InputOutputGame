using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // Start is called before the first frame update
    void Start()
    {
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

    /*IReadOnlyCollection<BluetoothDeviceInfo> SearchDevices()
    {
        BluetoothClient btClient = new BluetoothClient();

        return btClient.
    }*/

    IEnumerable<BluetoothDeviceInfo> GetPairedDevices()
    {
        BluetoothClient btClient = new BluetoothClient();

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

        selectTextObj.GetComponent<TextMeshProUGUI>().text = selectedDevice.DeviceName;
    }
}
