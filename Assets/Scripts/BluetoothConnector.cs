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
    public int EmgValue { get; private set; }
    public float EMGValueNormalized { get; private set; }

    private bool _isConnected;
    private bool _isScanning;
    private bool _isConnecting;
    private string _data;
    private LinkedList<BluetoothDevice> _devices;
    private BluetoothHelperCharacteristic _characteristic;
    private BluetoothHelperCharacteristic _emgCharacteristic;
    private EMSConnectCell _cell;
    public float LowBound { get; set; }
    public float HighBound { get; set; }
    public bool BoundAssigned => !float.IsNaN(LowBound) && !float.IsNaN(HighBound);
    public string DeviceName => GetComponentInParent<EMSConnectCell>().deviceTitle.text;
    public List<Tuple<string,List<int>>> emgValueSet = new();
    
    private BluetoothHelper _helper;
    private List<int> tempEmgValues = new();

    public void SetLowBound(string bound)
    {
        if (bound.IsNullOrEmpty()) return;
        LowBound = float.Parse(bound);
    }
    public void SetHighBound(string bound)
    {
        if (bound.IsNullOrEmpty()) return;
        HighBound = float.Parse(bound);
    }
    
    private void Awake()
    {
        _cell = GetComponentInParent<EMSConnectCell>();
        LowBound = float.NaN;
        HighBound = float.NaN;
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
        _isConnected = true;
        ConnectDevicePanel.Instance.deviceDetailPanel.ConnectedToDevice(_helper.getDeviceName());

        if (GameManager.Current.Device == GameManager.Device.Seeeduino)
        {
            var service = new BluetoothHelperService("19B10000-E8F2-537E-4F6C-D104768A1214");
            _characteristic = new BluetoothHelperCharacteristic("19B10001-E8F2-537E-4F6C-D104768A1214");
            _emgCharacteristic = new BluetoothHelperCharacteristic("19B10002-E8F2-537E-4F6C-D104768A1214");
            service.addCharacteristic(_characteristic);
            service.addCharacteristic(_emgCharacteristic);
            helper.Subscribe(service);
            helper.OnCharacteristicChanged += HelperOnOnCharacteristicChanged;
        }
        else
        {
            helper.StartListening();
        }
        
        //SendBluetoothData("r");
    }

    private void HelperOnOnCharacteristicChanged(BluetoothHelper helper, byte[] value, BluetoothHelperCharacteristic characteristic)
    {
        //if (!Equals(characteristic, _emgCharacteristic)) return;
        EmgValue = BitConverter.ToInt32(value);
        //print("EMG Value: " + EmgValue);
        if (BoundAssigned)
        {
            EMGValueNormalized = (EmgValue - LowBound) / (HighBound - LowBound);
        }
        else
        {
            EMGValueNormalized = EmgValue * 0.03f;
        }
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
        helper.OnCharacteristicChanged -= HelperOnOnCharacteristicChanged;
        _isConnected = false;
    }

    public void Disconnect()
    {
        _isConnected = false;
        _helper?.Disconnect();
        LowBound = float.NaN;
        HighBound = float.NaN;
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

    private void Update()
    {
#if UNITY_EDITOR
        EMGValueNormalized = Window_Graph.GetVirtualValue();  
#endif
        if(_isConnected)
            _helper.ReadCharacteristic(_emgCharacteristic);
    }

    public void CountEmgValue()
    {
        tempEmgValues.Add(EmgValue);
    }

    public void StopCountEmgValue(string key)
    {
        var valueList = new List<int>(tempEmgValues);
        emgValueSet.Add(new Tuple<string, List<int>>(key, valueList));
        tempEmgValues.Clear();
    }
}
