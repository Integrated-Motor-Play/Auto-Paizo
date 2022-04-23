using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogOut : MonoBehaviour
{
    public void OnLogOutButtonClicked()
    {
        PlayerPrefs.SetString("PlayerName", string.Empty);
        PhotonNetwork.Disconnect();
        Destroy(FindObjectOfType<BluetoothManager>().gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
}
