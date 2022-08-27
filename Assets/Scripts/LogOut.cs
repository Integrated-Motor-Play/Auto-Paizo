using System.Collections;
using System.Collections.Generic;
using Managers;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogOut : MonoBehaviour
{
    public void OnLogOutButtonClicked()
    {
        PlayerPrefs.SetString("PlayerName", string.Empty);
        if (GameManager.Current.Network == GameManager.NetworkMode.Online)
        {
            PhotonNetwork.Disconnect();
            Destroy(FindObjectOfType<PlayerManager>().gameObject);
        }
        Destroy(FindObjectOfType<BluetoothManager>().gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        print("Logged out");
    }
}
