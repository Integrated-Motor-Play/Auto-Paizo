using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ControlSettingContent : MonoBehaviour
{
    public Transform PlSlotPrefab;
    private void OnEnable()
    {
        foreach (var pl in PhotonNetwork.PlayerList)
        {
            var slot = Instantiate(PlSlotPrefab);
            slot.SetParent(transform);
            var slotControl = slot.GetComponent<PlayerToggleSlot>();
            slotControl.SlotPlayer = pl;
            if (GameManager.ControledPlayer.Contains(pl))
                slotControl.SlotToggle.isOn = true;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
