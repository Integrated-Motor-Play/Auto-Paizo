using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerToggleSlot : MonoBehaviour
{
    public Player SlotPlayer;
    public TextMeshProUGUI PlayerName;
    public Toggle SlotToggle;

    private void Start()
    {
        SlotToggle.isOn = GameManager.ControledPlayer.Contains(SlotPlayer);
    }

    public void OnToggleChange(bool isOn)
    {
        if (isOn)
        {
            if (!GameManager.ControledPlayer.Contains(SlotPlayer))
                GameManager.ControledPlayer.Add(SlotPlayer);
        }
        else
        {
            if (GameManager.ControledPlayer.Contains(SlotPlayer))
                GameManager.ControledPlayer.Remove(SlotPlayer);
        }
    }

    private void Update()
    {
        if (SlotPlayer != null)
        {
            PlayerName.text = SlotPlayer.NickName;
        }
    }
}
