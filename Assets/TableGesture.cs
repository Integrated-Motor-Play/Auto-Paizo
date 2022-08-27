using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TableGesture : MonoBehaviour
{
    public Gesture Gesture
    {
        get => _gesture;
        set
        {
            _gesture = value;
            icon.sprite = _gesture.icon;
        }
    }

    private Gesture _gesture;

    public EMSConnectCell.Hand hand;
    public BluetoothConnector Connector { get; set; }
    public Image icon;
    public Image matchedCircle;

    public void PlayMatchedAnimation()
    {
        matchedCircle.DOFade(1, 0.5f).SetEase(Ease.OutBounce);
    }

    private void OnDisable()
    {
        var color = matchedCircle.color;
        color.a = 0;
        matchedCircle.color = color;
    }
}
