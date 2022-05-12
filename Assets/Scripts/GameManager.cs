using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static string filePrefix;
    public static string playerName;
    public static GameMode currentMode;
    public static NetworkMode networkMode;
    public static List<Player> ControledPlayer = new List<Player>();
    public enum GameMode
    {
        HotHands,
        OddsAndEvens,
        RockPaperScissors,
        TwentyOne,
    }

    public enum NetworkMode
    {
        offline,
        online
    }

    public GameObject warningPanel;
    public float buzzerTime = 1800;
    private bool activated;
    //private int playerCount;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        filePrefix = System.DateTime.Now.ToShortTimeString().Replace(":", "-");
        playerName = PlayerPrefs.GetString("PlayerName");
        if (playerName != string.Empty)
        {
            DataRecord.GenerateCSVFile(GameManager.playerName + "_ScreenTime_" + GameManager.filePrefix, "Panel,Time");
        }
    }

    private void Update()
    {
        if (Time.time > buzzerTime && !activated)
        {
            warningPanel.SetActive(true);
            activated = true;
        }

        //if (PhotonNetwork.CurrentRoom == null) return;
        //if (PhotonNetwork.CurrentRoom.PlayerCount < playerCount)
        //{
        //    PhotonNetwork.Disconnect();
        //   SceneManager.LoadScene(0);
        //}
        //playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
    }

    public void SendBluetoothDataToPlayer(List<Player> player, string data)
    {
        // 让 Player 的 GameManager 执行 Receive 指令
        foreach (var pl in FindObjectsOfType<NetworkPlayer>())
        {
            if (!pl.photonView.IsMine) continue;
            pl.playerActions = new Dictionary<string, string>();
            print("Found players :" + player.Count);
            for (int i = 0; i < player.Count; i++)
            {
                print("User ID: " + player[i].UserId);
                pl.playerActions.Add(player[i].UserId, data);
            }
        }
    }

    public static void SendBluetoothData(string data)
    {
#if UNITY_EDITOR
        print("Sended Bluetooth Message: " + data);

#else
        BluetoothManager.helper.SendData(data);
#endif
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        SendBluetoothData("r");
    }
}
