using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonHover : MonoBehaviour
{
    private Image _image;
    public float fadeTime = 0.5f;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    private static bool CheckPlayButton()
    {
        var parent = FindObjectOfType<CellParent>();
        return parent != null && parent.GetComponentsInChildren<EMSConnectCell>().All(cell => cell.Connected);
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
