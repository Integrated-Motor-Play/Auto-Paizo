using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EMSConnectCell : MonoBehaviour
{
    public TextMeshProUGUI deviceTitle;
    public BluetoothConnector connector;
    private string _title;

    private void Awake()
    {
        _title = "Unconnected " + (transform.GetSiblingIndex() + 1);
        deviceTitle.text = _title;
    }

    public void MouseClicked()
    {
        var detailPanel = ConnectDeviceUpPanel.Instance.deviceDetailPanel;
        detailPanel.gameObject.SetActive(true);
        detailPanel.ShowPanel(transform.position, _title, this);
    }

    public void UpdateName(string deviceName)
    {
        deviceTitle.text = deviceName;
    }

    public void Reconnect()
    {
        UpdateName(_title);
        ConnectDeviceUpPanel.Instance.deviceDetailPanel.ShowPanel(transform.position, _title, this);
    }
}
