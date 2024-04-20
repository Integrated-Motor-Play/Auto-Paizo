using System.Collections;
using System.Collections.Generic;
using ArduinoBluetoothAPI;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void TurnOffEMS()
    {
        if(GameManager.Current.Mode == GameManager.Mode.SinglePlayer)
            CalibrationController.SendLocalBluetoothData("r");
    }

    public void DisconnectBluetooth()
    {
        if(GameManager.Current.Mode == GameManager.Mode.SinglePlayer)
            BluetoothManager.Instance.Disconnect();
        var synchronizer = FindObjectOfType<Synchronizer>();
        if( synchronizer != null)
            Destroy(synchronizer.gameObject);
        if(GameManager.Current.Mode == GameManager.Mode.SinglePlayer)
            Destroy(BluetoothManager.Instance.gameObject);
    }
}
