using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AnimationOnTextChange : MonoBehaviour
{
    public float amount = 2;
    public float time = 0.5f;
    public Ease ease = Ease.InBack;
    
    private TextMeshProUGUI _myText;
    private string _lastText;

    private void Awake()
    {
        _myText = GetComponent<TextMeshProUGUI>();
        _lastText = _myText.text;
    }

    private void Update()
    {
        if (_lastText != _myText.text)
        {
            //Animation
            transform.localScale = amount * transform.localScale;
            transform.DOScale(1, time).SetEase(ease);
        }

        _lastText = _myText.text;
    }
}
