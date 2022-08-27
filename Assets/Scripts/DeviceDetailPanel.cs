using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using ArduinoBluetoothAPI;
using UnityEngine;
using DG.Tweening;
using Managers;
using TMPro;
using Unity.Mathematics;
using WebSocketSharp;

public class DeviceDetailPanel : MonoBehaviour
{
    public Transform buttonPrefab;
    public TextMeshProUGUI deviceTitle;
    public GameObject scanningCircle;
    public GameObject retryButton;
    public TextMeshProUGUI contentText;
    public GameObject reminderPanel;
    public float fadeTime = 0.5f;
    public Transform buttonParent;

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
        _cell.connector.ClearDevices();
        ScanBluetooth();
        EnableScanAnimation(true);
        SetContextText("");
        EnableRetryButton(false);
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
    
    public void DrawButtons(LinkedList<BluetoothDevice> devices)
    {
        EnableScanAnimation(false);
        var node = devices.First;
        for (var i = 0; i < 16; i++)
        {
            var bluetoothName = node.Value.DeviceName;
            var button = Instantiate(buttonPrefab, transform.position, quaternion.identity);
            button.SetParent(ConnectDevicePanel.Instance.deviceDetailPanel.buttonParent);
            if(bluetoothName.Contains("Paizo"))
                button.SetSiblingIndex(0);
            button.GetComponent<BluetoothDeviceButton>().SetButtonNameAndConnector(bluetoothName, _cell.connector);
                
            node = node.Next;
            if (node == null)
                return;
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

    public void ConnectedToDevice(string deviceName)
    {
        SetContextText("Connected to\n" + deviceName + "!");
        deviceTitle.text = deviceName;
        _cell.SetConnectedButton(true);
        _cell.UpdateName(deviceName);
        _cell.SetConnected(true);
        HidePanel();
    }

    public void EnableScanAnimation(bool enable)
    {
        scanningCircle.SetActive(enable);
    }

    public void SetContextText(string text)
    {
        contentText.text = text;
    }

    public void RetryConnect()
    {
        if (BluetoothDeviceButton.ButtonName.IsNullOrEmpty()) return;
        _cell.connector.Connect(BluetoothDeviceButton.ButtonName);
        SetContextText("Retry Connecting to\n" + BluetoothDeviceButton.ButtonName + "\n...");
    }

    public void EnableRetryButton(bool enable)
    {
        retryButton.SetActive(enable);
    }
}
