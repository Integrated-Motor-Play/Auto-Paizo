using System;
using System.Collections.Generic;
using ArduinoBluetoothAPI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class BluetoothManager : MonoBehaviour
    {

        public GUIStyle style;
        private bool _isScanning;
        private bool _isConnecting;
        private string _data;
        private LinkedList<BluetoothDevice> _devices;
        private BluetoothHelperCharacteristic _characteristic;
        private bool _connected = true;

        private static BluetoothHelper _helper;
        private static BluetoothManager _instance;

        public static BluetoothManager Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<BluetoothManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<BluetoothManager>();
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
            }
            DontDestroyOnLoad(this);
        }

        void Start()
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

        void OnDataReceived(BluetoothHelper helper)
        {
            _data += "\n<" + helper.Read();
            print("Received: " + _data);
        }

        void OnScanEnded(BluetoothHelper helper, LinkedList<BluetoothDevice> devices)
        {
            this._isScanning = false;
            this._devices = devices;
        }

        void OnConnected(BluetoothHelper helper)
        {
            _isConnecting = false;

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

            SendBluetoothData("r");
        }
    

        public static void SendBluetoothData(string d)
        {
#if UNITY_EDITOR
            print("Sent Bluetooth Message: " + d);
#else
            SendDataToDevice(d);
#endif
        }

        private static void SendDataToDevice(string d)
        {
            if (GameManager.Current.Device == GameManager.Device.Seeeduino)
                _helper.WriteCharacteristic(_instance._characteristic, d);
            else
                _helper.SendData(d);
        }
    
        void OnConnectionFailed(BluetoothHelper helper)
        {
            _isConnecting = false;
            Debug.Log("Connection lost");
        }


        void OnGUI()
        {
#if UNITY_EDITOR
            return;
#else
            DrawGUI();
#endif
        }

        private void DrawGUI()
        {
            if (_helper == null)
                return;
            if (!_helper.isConnected() && !_isScanning && !_isConnecting)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 4, Screen.height / 10, Screen.width / 2, Screen.height / 10), "Start Scanning", style))
                {
                    _isScanning = _helper.ScanNearbyDevices();
                }
                if (_devices != null && _devices.First != null)
                {
                    Draw();
                }
            }
            else if (!_helper.isConnected() && _isScanning)
            {
                GUI.TextArea(new Rect(Screen.width / 2 - Screen.width / 4, Screen.height / 10, Screen.width / 2, Screen.height / 10), "Scanning...", style);
            }
            else if (_helper.isConnected() && _connected)
            {
                _connected = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        private void Draw()
        {
            var node = _devices.First;
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 1; j++)
                {
                    var bluetoothName = node.Value.DeviceName;
                    if (GUI.Button(new Rect((j + 1) * Screen.width / 4 + 5, (i + 2) * Screen.height / 10 + 5, Screen.width / 2 - 10, Screen.height / 10 - 10), bluetoothName + " - " + node.Value.Rssi, style))
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
                    node = node.Next;
                    if (node == null)
                        return;
                }
            }
        }

        void OnDestroy()
        {
            _helper?.Disconnect();
        }
    }
}
