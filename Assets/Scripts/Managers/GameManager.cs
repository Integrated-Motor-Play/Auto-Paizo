using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public static string FilePrefix;
        public static string PlayerName;
        public static Mode CurrentMode;
        public static NetworkMode networkMode;
        public static List<Player> ControledPlayer = new List<Player>();
        public static Device CurrentDevice;
        public enum GameMode
        {
            SlapMeIfYouCan,
            Numbers,
            Elements,
            TwentyOne,
            BlackJack,
            TriangularMatch
        }

        public enum Device
        {
            Seeeduino,
            Adafruit,
        }

        public enum NetworkMode
        {
            Offline,
            Online
        }

        public static GameManager Instance;

        public struct Mode
        {
            public GameMode GameMode;

            public string ModeName
            {
                get
                {
                    var name = GameMode switch
                    {
                        GameMode.SlapMeIfYouCan => "Hot Hands",
                        GameMode.Numbers => "Numbers",
                        GameMode.Elements => "Elements",
                        GameMode.TwentyOne => "Twenty One",
                        GameMode.BlackJack => "BlackJack",
                        GameMode.TriangularMatch => "Triangular Match",
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    return name;
                }
            }
        }

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

            FilePrefix = System.DateTime.Now.ToShortTimeString().Replace(":", "-");
            PlayerName = PlayerPrefs.GetString("PlayerName");
            if (PlayerName != string.Empty)
            {
                DataRecord.GenerateCSVFile(GameManager.PlayerName + "_ScreenTime_" + GameManager.FilePrefix, "Panel,Time");
            }
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
            BluetoothManager.SendBluetoothData(data);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            SendBluetoothData("r");
        }
    }
}
