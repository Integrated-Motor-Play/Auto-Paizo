using System;
using System.Collections.Generic;
using ArduinoBluetoothAPI;
using UnityEngine;
using System.Collections;
using Managers;
using UnityEngine.UI;

public class BluetoothConnector : MonoBehaviour
{
    public Transform buttonPrefab;

    private bool _isScanning;
    private bool _isConnecting;
    private string _data;
    private LinkedList<BluetoothDevice> _devices;
    private BluetoothHelperCharacteristic _characteristic;
    public bool Connected;
    private EMSConnectCell _emsConnectCell;
    
    private BluetoothHelper _helper;

    private void Awake()
    {
        _emsConnectCell = GetComponentInParent<EMSConnectCell>();
    }

    private void Start()
    {
        _data = "";
        try
        {
            BluetoothHelper.BLE = true;
            _helper = BluetoothHelper.GetInstance();
            _helper.OnConnected += OnConnected;
            _helper.OnConnectionFailed += OnConnectionFailed;
            _helper.OnScanEnded += OnScanEnded;
            _helper.OnDataReceived += OnDataReceived;

            _helper.setTerminatorBasedStream("\n"); //every messages ends with new line character

        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

    }

    private void OnDataReceived(BluetoothHelper helper)
    {
        _data += "\n<" + helper.Read();
        print("Received: " + _data);
    }

    private void OnScanEnded(BluetoothHelper helper, LinkedList<BluetoothDevice> devices)
    {
        this._isScanning = false;
        this._devices = devices;
    }

    private void OnConnected(BluetoothHelper helper)
    {
        _isConnecting = false;
        Connected = true;
        ConnectDeviceUpPanel.Instance.deviceDetailPanel.UpdateName(_helper.getDeviceName());
        _emsConnectCell.UpdateName(_helper.getDeviceName());
        
        if (GameManager.CurrentDevice == GameManager.Device.Seeeduino)
        {
            var service = new BluetoothHelperService("19B10000-E8F2-537E-4F6C-D104768A1214");
            _characteristic = new BluetoothHelperCharacteristic("19B10001-E8F2-537E-4F6C-D104768A1214");
            service.addCharacteristic(_characteristic);
            helper.Subscribe(service);
        }
        else
        {
            helper.StartListening();
        }

        _devices = null;
        SendBluetoothData("r");
    }
    
    public void SendBluetoothData(string d)
    {
#if UNITY_EDITOR
        print("Sent Bluetooth Message: " + d);
#else
            SendDataToDevice(d);
#endif
    }
    
    private void SendDataToDevice(string d)
    {
        if (GameManager.CurrentDevice == GameManager.Device.Seeeduino)
            _helper.WriteCharacteristic(_characteristic, d);
        else
            _helper.SendData(d);
    }

    private void OnConnectionFailed(BluetoothHelper helper)
    {
        _isConnecting = false;
        Connected = false;
        _devices = null;
        Debug.Log("Connection lost");
    }

    public void Disconnect()
    {
        _devices = null;
        _helper?.Disconnect();
    }

    private void OnDestroy()
    {
        Disconnect();
        Connected = false;
    }

    public void ScanBluetooth()
    {
        print("Scanning...");
        if (_helper == null)
        {
            print("Helper not found");
            return;
        }
        _isScanning = _helper.ScanNearbyDevices();
        StartCoroutine(WaitToFinishScan());
    }

    private IEnumerator WaitToFinishScan()
    {
        yield return new WaitUntil(() => _devices != null && _devices.First != null);
        DrawButtons();
        //DirConnect();
    }

    private void DrawButtons()
    {
        var node = _devices.First;
        for (var i = 0; i < 16; i++)
        {
            var bluetoothName = node.Value.DeviceName;
                var button = Instantiate(buttonPrefab, ConnectDeviceUpPanel.Instance.deviceDetailPanel.buttonParent, true);
                button.GetComponent<BluetoothDeviceButton>().SetButtonName(bluetoothName, this);
                
                node = node.Next;
                if (node == null)
                    return;
        }
    }

    public void OnButtonPressed(string bluetoothName)
    {
        _helper.setDeviceName(bluetoothName);
        try
        {
            _helper.Connect();
            _isConnecting = true;
        }
        catch (Exception)
        {
            _isConnecting = false;
        }
    }

    private void DirConnect()
    {
        var node = _devices.First;
        print("Found Device: " + node.Value.DeviceName);

        var bluetoothName = node.Value.DeviceName;
        _helper.setDeviceName(bluetoothName);
        try
        {
            _helper.Connect();
            _isConnecting = true;
        }
        catch (Exception)
        {
            _isConnecting = false;
        }
    }


}
