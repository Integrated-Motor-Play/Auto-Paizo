using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using ArduinoBluetoothAPI;
using UnityEngine;
using DG.Tweening;
using TMPro;
using WebSocketSharp;

public class DeviceDetailPanel : MonoBehaviour
{
    public TextMeshProUGUI deviceTitle;
    public GameObject reminderPanel;
    public float fadeTime = 0.5f;
    public Transform buttonParent;
    public GameObject ReconnectButton;

    private Vector3 _iniPosition;
    private Vector3 _cellPosition;

    private EMSConnectCell _cell;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _iniPosition = transform.position;
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void ShowPanel(Vector3 cellPosition, string title, EMSConnectCell cell)
    {
        transform.DOKill();
        _cellPosition = cellPosition;
        transform.position = cellPosition;
        transform.localScale = Vector3.zero;
        deviceTitle.text = title;
        _cell = cell;
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, fadeTime);
        transform.DOScale(1, fadeTime);
        transform.DOMove(_iniPosition, fadeTime);

        if (!_cell.connector.Connected)
        {
            ScanBluetooth();
            ReconnectButton.SetActive(false);
        }
        else
            ReconnectButton.SetActive(true);
    }

    public void HidePanel()
    {
        _canvasGroup.DOFade(0, fadeTime);
        transform.DOScale(0, fadeTime);
        transform.DOMove(_cellPosition, fadeTime);
        for (var i = 0; i < buttonParent.childCount; i++)
        {
            var btn = buttonParent.GetChild(i).gameObject;
            Destroy(btn);
        }
    }

    public void ScanBluetooth()
    {
        try{
            _cell.connector.ScanBluetooth();
        } 
        catch(BluetoothHelper.BlueToothNotEnabledException)
        {
            //warning
            reminderPanel.SetActive(true);
        }
    }

    public void UpdateName(string deviceName)
    {
        deviceTitle.text = deviceName;
        HidePanel();
    }

    public void Reconnect()
    {
        _cell.connector.Disconnect();
        _cell.Reconnect();
        ScanBluetooth();
    }
}
