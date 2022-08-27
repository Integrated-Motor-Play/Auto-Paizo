using System;
using Managers;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class CalibrationController : MonoBehaviour
{
    private BluetoothConnector _connector;
    public Toggle[] toggles;
    public TextMeshProUGUI deviceTitle;

    public void SetupCalibration(BluetoothConnector connector, string deviceName)
    {
        _connector = connector;
        deviceTitle.text = deviceName;
    }

    public void Channel1(bool on)
    {
        SendBluetoothData(on ? "A" : "a");
    }

    public void Channel2(bool on)
    {
        SendBluetoothData(on ? "B" : "b");
    }

    public void Channel3(bool on)
    {
        SendBluetoothData(on ? "C" : "c");
    }
    
    public void Channel4(bool on)
    {
        SendBluetoothData(on ? "D" : "d");
    }

    private void ResetAll()
    {
        foreach (var gm in toggles)
        {
            gm.SetIsOnWithoutNotify(false);
        }
    }

    private void OnDisable()
    {
        SendBluetoothData("r");
        ResetAll();
    }

    public static void SendLocalBluetoothData(string data)
    {
        if (GameManager.Current.Network == GameManager.NetworkMode.Online)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
                GameManager.Instance.SendBluetoothDataToPlayer(GameManager.ControledPlayer, data);
            else
                GameManager.SendBluetoothData(data);
            print(data);
        }
        else
        {
            GameManager.SendBluetoothData(data);
        }
    }
    
    private void SendBluetoothData(string data)
    {
        if(_connector != null)
            _connector.SendBluetoothData(data);
    }
}

