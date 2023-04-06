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
    [SerializeField] private bool openSerial = false;
    
    [Space]
    
    [SerializeField] private string data;
    [SerializeField] private bool serialConnected = false;

    [Space]
    
    [SerializeField] private List<string> ports;
    [SerializeField] private  string selectedPortName;
    [SerializeField] private  int baudrate;

    [Space]
    
    private TMP_Dropdown dropdown;
    private SerialPort Serial;

    private bool dropdownLoaded = false;

    // Start is called before the first frame update
    void Awake()
    {
        Serial = new SerialPort();
        
        string[] _ports = SerialPort.GetPortNames();
        foreach (string port in _ports)
        {
            ports.Add(port);
        }

        dropdown = GameObject.Find("SSPDropdown").GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        dropdown.AddOptions(ports);

    }

    // Update is called once per frame
    void Update()
    {
        if (openSerial)
        {
            openSerial = false;
            ConfirmPortSelection();
        }
        
        try
        {
            selectedPortName = ports[dropdown.value];
        }
        catch { }

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
            WriteString("READY");
            serialConnected = true;
        }
        else
        {
            Debug.Log("Connection failed - closing serial");
            Serial.Close();
            serialConnected = false;
        }
    }

    public void ConfirmPortSelection()
    {
        OpenSerial(selectedPortName, baudrate, Parity.None, 8, StopBits.One);
    }

    public void WriteString(string line)
    {
        Serial.WriteLine(line);
    }

    public bool GetSerialStatus()
    {
        return serialConnected;
    }
}
