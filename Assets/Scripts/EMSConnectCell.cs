using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EMSConnectCell : MonoBehaviour
{
    public int index;
    public Hand hand;
    public Player player;

    public bool Connected { get; private set; }
    public Transform handIcon;
    public TextMeshProUGUI deviceTitle;
    public BluetoothConnector connector;
    public GameObject beforeConnectPanel, afterConnectPanel;
    public UnityEvent OnConnected;
    
    private string _title;
    private string _defaultName;

    public enum Hand
    {
        LeftHand,
        RightHand
    }

    public enum Player
    {
        P1,
        P2
    }

    private void Awake()
    {
        _defaultName = "Device " + index;
        _title = deviceTitle.text;
    }

    private void OnValidate()
    {
        _defaultName = "Device " + index;
        deviceTitle.text = _defaultName;
        handIcon.localScale = new Vector3((hand == Hand.LeftHand) ? 1 : -1, 1, 1);
    }

    public void ConnectButtonClicked()
    {
        var detailPanel = ConnectDevicePanel.Instance.deviceDetailPanel;
        detailPanel.gameObject.SetActive(true);
        detailPanel.ShowPanel(transform.position, _title, this);
    }
    
    public void DisconnectButtonClicked()
    {
        connector.Disconnect();
        SetConnectedButton(false);
        deviceTitle.text = _title;
        SetConnected(false);
    }
    
    public void CalibrationButtonClicked()
    {
        var calibration = ConnectDevicePanel.Instance.calibrationPanel;
        calibration.gameObject.SetActive(true);
        calibration.SetupCalibration(connector, deviceTitle.text);
    }

    public void UpdateName(string deviceName)
    {
        deviceTitle.text = deviceName;
    }

    public void SetConnectedButton(bool active)
    {
        beforeConnectPanel.SetActive(!active);
        afterConnectPanel.SetActive(active);
    }

    public void SetConnected(bool isConnected)
    {
        Connected = isConnected;
        OnConnected.Invoke();
    }
    
    private void OnDestroy()
    {
        DisconnectButtonClicked();
    }

    private void OnDisable()
    {
        DisconnectButtonClicked();
    }
}
