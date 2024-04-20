using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    private Image _image;
    public float fadeTime = 0.5f;
    public bool canReveal = true;

    private EMSConnectCell[] _cells;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
        var parent = FindObjectOfType<CellParent>();
        if (parent == null) return;
        _cells = parent.GetComponentsInChildren<EMSConnectCell>();
        foreach (var cell in _cells)
        {
            cell.OnConnected.AddListener(TryToRevealButton);
        }
    }

    private bool CheckPlayButton()
    {
        var parent = FindObjectOfType<CellParent>();
        return parent != null 
               && parent.GetComponentsInChildren<EMSConnectCell>().All(cell => cell.Connected)
               && canReveal;
    }

    public void TryToRevealButton()
    {
        var allConnected = CheckPlayButton();
        _image.raycastTarget = !allConnected;
        var alpha = allConnected ? 0 : 0.8f;
        if (!DOTween.IsTweening(_image) && Math.Abs(_image.color.a - alpha) > 0.1f)
            _image.DOFade(alpha, fadeTime);
    }
}
