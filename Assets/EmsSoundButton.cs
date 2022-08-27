using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmsSoundButton : MonoBehaviour
{
    public TextMeshProUGUI label;
    public Image icon;

    public enum SoundState
    {
        SoundOff,
        SoundSame,
        SoundDifferent
    }

    [Serializable]
    public class Sound
    {
        public string name;
        public SoundState state;
        public Sprite icon;
        public string bluetoothData;
    }

    public Sound[] soundStates;
    
    public SoundState currentState;

    private void OnEnable()
    {
        SwitchState(SoundState.SoundOff);
    }

    private void SwitchState(SoundState state)
    {
        currentState = state;

        var obj = UpdateIconAndText();
        var connectors = FindObjectsOfType<BluetoothConnector>();
        foreach (var connector in connectors)
        {
            connector.SendBluetoothData(obj.bluetoothData);
        }
    }

    public void SwitchState()
    {
        SwitchState(currentState.Next());
    }

    private Sound UpdateIconAndText()
    {
        var stateObj = soundStates.ToList().Find(s => s.state == currentState);
        label.text = stateObj.name;
        icon.sprite = stateObj.icon;
        return stateObj;
    }

    private void OnValidate()
    {
        UpdateIconAndText();
    }
}
