using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationZoomIn : MonoBehaviour
{
    public float amount = 2;
    public float time = 0.5f;
    private Vector3 _iniScale;

    private void Awake()
    {
        _iniScale = transform.localScale;
    }

    public void PlayAnimation()
    {
        DOTween.Kill(transform);
        transform.localScale = amount * transform.localScale;
        transform.DOScale(_iniScale, time).SetEase(Ease.InBack);
    }
}
