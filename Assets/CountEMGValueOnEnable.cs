using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountEMGValueOnEnable : MonoBehaviour
{
    public string countKey;
    private BluetoothConnector[] _bluetoothConnectors;

    private void Awake()
    {
        _bluetoothConnectors = FindObjectsOfType<BluetoothConnector>();
    }

    private void Update()
    {
        foreach (var connector in _bluetoothConnectors)
        {
            connector.CountEmgValue();
        }
    }

    private void OnDisable()
    {
        foreach (var connector in _bluetoothConnectors)
        {
            connector.StopCountEmgValue(countKey);
        }
        
        var recorder = DataRecord.LoadJsonFromFile<SocialGameRecord>("data") ?? new SocialGameRecord();
        recorder.AddGame(new SocialGameRecord.GameRecord());
        DataRecord.GenerateNewFile("data", "json", JsonUtility.ToJson(recorder, true));
    }
}
