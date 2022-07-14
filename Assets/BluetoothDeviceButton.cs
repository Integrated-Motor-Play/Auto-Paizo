using System.Collections;
using System.Collections.Generic;
using ArduinoBluetoothAPI;
using TMPro;
using UnityEngine;

public class BluetoothDeviceButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    private string _buttonName;
    private BluetoothConnector _connector;

    public void SetButtonName(string btnName, BluetoothConnector connector)
    {
        buttonText.text = btnName;
        _buttonName = btnName;
        _connector = connector;
    }
    
    public void OnButtonPressed()
    {
        _connector.OnButtonPressed(_buttonName);
    }
}
