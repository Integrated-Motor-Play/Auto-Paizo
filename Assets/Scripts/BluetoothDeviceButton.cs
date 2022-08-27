using System.Collections;
using System.Collections.Generic;
using ArduinoBluetoothAPI;
using TMPro;
using UnityEngine;

public class BluetoothDeviceButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public static string ButtonName;
    private string _btnName;
    private BluetoothConnector _connector;

    public void SetButtonNameAndConnector(string btnName, BluetoothConnector connector)
    {
        buttonText.text = btnName;
        _btnName = btnName;
        _connector = connector;
    }
    
    public void OnButtonPressed()
    {
        ButtonName = _btnName;
        ConnectDevicePanel.Instance.deviceDetailPanel.SetContextText("Connecting to\n" + ButtonName + "\n...");
        _connector.ClearDevices();
        _connector.Connect(ButtonName);
        for (var i = 0; i < transform.parent.childCount; i++)
        {
            Destroy(transform.parent.GetChild(i).gameObject);
        }
    }
}
