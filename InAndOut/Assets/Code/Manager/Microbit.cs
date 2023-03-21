using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using Shapes2D;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Microbit : MonoBehaviour
{
    [SerializeField] private bool sendData = false;
    
    [SerializeField] private string data;

    [Space]
    
    [SerializeField] private List<string> ports;
    [SerializeField] private  string selectedPortName;
    [SerializeField] private  int baudrate;

    [Space]
    
    [SerializeField] private  TMP_Dropdown dropdown;

    private SerialPort Serial;

    // Start is called before the first frame update
    void Start()
    {
        Serial = new SerialPort();
        
        string[] _ports = SerialPort.GetPortNames();
        foreach (string port in _ports)
        {
            ports.Add(port);
        }

        try
        {
            dropdown = GameObject.Find("SSPDropdown").GetComponent<TMP_Dropdown>();
            dropdown.ClearOptions();
            dropdown.AddOptions(ports);
        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            selectedPortName = ports[dropdown.value];
        }
        catch { }

        if (sendData)
        {
            sendData = false;
            Debug.Log("Sending data through Serial Port " + Serial.PortName);
            Serial.WriteLine("TEST");
            Debug.Log("Data sent");
        }
    }
    
    void OpenSerial(string aPortName, int aBaudrate, Parity aParity, int aDataBits, StopBits aStopBits)
    {
        Debug.Log("Starting serial connection through port: " + aPortName);
        
        Serial.PortName = aPortName;
        Serial.BaudRate = aBaudrate;
        Serial.Parity = aParity;
        Serial.DataBits = aDataBits;
        Serial.StopBits = aStopBits;

        Serial.Open();

        if (Serial.IsOpen)
        {
            Debug.Log("Connection successful");
        }
    }

    public void ConfirmPortSelection()
    {
        OpenSerial(selectedPortName, baudrate, Parity.None, 8, StopBits.One);
    }
}
