using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using UnityEngine;

public class OldBluetoothHandler : MonoBehaviour
{
    /* Variables */

    private BluetoothDeviceInfo mDevice = null;

    /* Setter & Getter */

    /* Functions */

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Test();
    }

    private void Test()
    {
        var cli = new BluetoothClient();
        IReadOnlyCollection<BluetoothDeviceInfo> peers = cli.DiscoverDevices();

        print("Peers Length: " + peers.Count);

        if (peers.Count == 0)
        {
            Debug.Log("No device detected");
            return;
        }

        foreach (var deviceInfo in peers)
        {
            print("DN: " + deviceInfo.DeviceName);

            if (deviceInfo.DeviceName == "raspberrypi")
                mDevice = deviceInfo;
        }

        if (mDevice == null)
            return;

        print("Address: " + mDevice.DeviceAddress);

        cli.Connect(mDevice.DeviceAddress, BluetoothService.SerialPort);

        NetworkStream stream = cli.GetStream();

        string msg = "Hello World!~";
        byte[] data = Encoding.ASCII.GetBytes(msg);
        stream.Write(data, 0, data.Length);
    }
}
