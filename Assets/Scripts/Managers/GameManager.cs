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
        public static Gaming Current;
        public static List<Player> ControledPlayer = new List<Player>();
        public static bool Initialized;
        
        public Gaming DefaultGaming;

        public enum Game
        {
            SlapMeIfYouCan,
            Numbers,
            Elements,
            TwentyOne,
            BlackJack,
            Match2Players,
            Match1Player,
            ArmWrestling
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

        public enum Mode
        {
            SinglePlayer,
            Social,
            Computer,
            Plant
        }
        
        public enum Round
        {
            BestOf3 = 0,
            BestOf5 = 1,
            FreePlay = 2,
        }

        public static GameManager Instance;

        [Serializable]
        public struct Gaming
        {
            public Game Game;
            public Device Device;
            public NetworkMode Network;
            public Mode Mode;
            public Round Round;

            public string GameName
            {
                get
                {
                    var name = Game switch
                    {
                        Game.SlapMeIfYouCan => "Hot Hands",
                        Game.Numbers => "Numbers",
                        Game.Elements => "Godai",
                        Game.TwentyOne => "Twenty One",
                        Game.BlackJack => "Epta",
                        Game.Match2Players => "ídio 2 Players",
                        Game.Match1Player => "ídio 1 Player",
                        Game.ArmWrestling => "Arm Spark",
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    return name;
                }
            }
            
            public string ModeName
            {
                get
                {
                    var name = Mode switch
                    {
                        Mode.SinglePlayer => "Auto-Paizo Games",
                        Mode.Social => "Mazi Games",
                        Mode.Computer => "Théa Games",
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    return name;
                }
            }
            
            public string RoundName
            {
                get
                {
                    var name = Round switch
                    {
                        Round.BestOf3 => "Best of 3",
                        Round.BestOf5 => "Best of 5",
                        Round.FreePlay => "Free Play",
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    return name;
                }
            }

            public int RoundValue
            {
                get
                {
                    var value = Round switch
                    {
                        Round.BestOf3 => 3,
                        Round.BestOf5 => 5,
                        Round.FreePlay => 100,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    return value;
                }
            }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
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
            Current = DefaultGaming;
            Initialized = true;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
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
            if(BluetoothManager.Instance != null)
                BluetoothManager.SendBluetoothData(data);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            SendBluetoothData("r");
        }

        public static void SendChannelMessage(BluetoothConnector connector,int channel, bool on)
        {
            switch (channel)
            {
                case 0:
                    connector.SendBluetoothData(on ? "E" : "e");
                    break;
                case 1:
                    connector.SendBluetoothData(on ? "A" : "a");
                    break;
                case 2:
                    connector.SendBluetoothData(on ? "B" : "b");
                    break;
                case 3:
                    connector.SendBluetoothData(on ? "C" : "c");
                    break;
                case 4:
                    connector.SendBluetoothData(on ? "D" : "d");
                    break;
            }
        }
    }
}
