using System;
using System.Collections.Generic;
using ArduinoBluetoothAPI;
using UnityEngine;
using System.Collections;
using System.Linq;
using Managers;
using UISystem;
using UnityEngine.UI;
using WebSocketSharp;

public class BluetoothConnector : MonoBehaviour
{
    private bool _isScanning;
    private bool _isConnecting;
    private string _data;
    private LinkedList<BluetoothDevice> _devices;
    private BluetoothHelperCharacteristic _characteristic;
    private EMSConnectCell _cell;

    private BluetoothHelper _helper;

    private void Awake()
    {
        _cell = GetComponentInParent<EMSConnectCell>();
    }

    private void Start()
    {
        _data = "";
        try
        {
            BluetoothHelper.BLE = true;
            _helper = BluetoothHelper.GetNewInstance();
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
        ConnectDevicePanel.Instance.deviceDetailPanel.ConnectedToDevice(_helper.getDeviceName());

        if (GameManager.Current.Device == GameManager.Device.Seeeduino)
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
        
        //SendBluetoothData("r");
    }
    
    public void SendBluetoothData(string d)
    {
#if UNITY_EDITOR
        print("Sent Bluetooth Message: " + d);
#else
        print("Sent Bluetooth Message: " + d);
        SendDataToDevice(d);
#endif
    }
    
    private void SendDataToDevice(string d)
    {
        if (GameManager.Current.Device == GameManager.Device.Seeeduino)
            _helper.WriteCharacteristic(_characteristic, d);
        else
            _helper.SendData(d);
    }

    private void OnConnectionFailed(BluetoothHelper helper)
    {
        _isConnecting = false;
        Debug.Log("Connection lost");
        ConnectDevicePanel.Instance.deviceDetailPanel.SetContextText("Connection lost");
        ConnectDevicePanel.Instance.deviceDetailPanel.EnableRetryButton(true);
        _cell.DisconnectButtonClicked();
    }

    public void Disconnect()
    {
        _helper?.Disconnect();
    }

    public void ClearDevices()
    {
        if(_devices == null)
            return;
        _devices.Clear();
        _devices = null;
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
        ConnectDevicePanel.Instance.deviceDetailPanel.DrawButtons(_devices);
        //DirConnect();
    }

    public void Connect(string bluetoothName)
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
