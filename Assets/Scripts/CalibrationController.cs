using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class CalibrationController : MonoBehaviour
{
    public Toggle[] toggles;

    public void IndexFinger(bool on)
    {
        if (on) { SendBluetoothData("A"); }
        else { SendBluetoothData("a"); }
    }

    public void MiddleFinger(bool on)
    {
        if (on) { SendBluetoothData("C"); }
        else { SendBluetoothData("c"); }
    }

    public void RingFinger(bool on)
    {
        if (on) { SendBluetoothData("D"); }
        else { SendBluetoothData("d"); }
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

    public static void SendBluetoothData(string data)
    {
        if (GameManager.networkMode == GameManager.NetworkMode.online)
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
}

