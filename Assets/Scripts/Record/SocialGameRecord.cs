using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

[Serializable]
public class SocialGameRecord
{
    [Serializable]
    public class GameRecord
    {
        [Serializable]
        public class EMGData
        {
            public string Key;
            public int[] Data;
    
            public EMGData(string key, int[] data)
            {
                Key = key;
                Data = data;
            }
        }
        [Serializable]
        public class Player
        {
            public string DeviceName;
            public float LowBound, HighBound;
            public EMGData[] EmgData;
        }
    
        public string GameName;
        public string TimeWhenPlay;
        public string DeviceType;
        public string GameMode;
        public string GameRound;
        public Player[] Players;
    
        public GameRecord()
        {
            GameName = GameManager.Current.GameName;
            TimeWhenPlay = DateTime.Now.ToString("yyyy-MM-dd\\THH:mm:ss\\Z");
            DeviceType = GameManager.Current.Device.ToString();
            GameMode = GameManager.Current.ModeName;
            GameRound = GameManager.Current.RoundName;
            var connectors = GameObject.FindObjectsOfType<BluetoothConnector>().ToList();
    
            var pls = new List<Player>();
            foreach (var connector in connectors)
            {
                var pl = new Player
                {
                    DeviceName = connector.DeviceName,
                    LowBound = connector.LowBound,
                    HighBound = connector.HighBound
                };
                var valueSet = connector.emgValueSet;
                var data = valueSet.Select(t => new EMGData(t.Item1, t.Item2.ToArray())).ToList();
                pl.EmgData = data.ToArray();
                
                pls.Add(pl);
            }
    
            Players = pls.ToArray();
        }
    }
    
    public GameRecord[] Games = Array.Empty<GameRecord>();

    public void AddGame(GameRecord gameRecord)
    { 
        var gms = Games.ToList();
        gms.Add(gameRecord);
        Games = gms.ToArray();
    }

    public SocialGameRecord()
    {
        
    }
}
