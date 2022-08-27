using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnimationLeadInAndOut : MonoBehaviour
{
    public CanvasGroup graphic;
    public float distance;
    public float time;

    private Vector3 iniPos;

    private void Awake()
    {
        iniPos = transform.position;
    }

    private void OnEnable()
    {
        var positionTemp = iniPos;
        positionTemp.y += distance * 0.5f * Screen.height;
        transform.position = positionTemp;
        graphic.alpha = 0;
        transform.DOMoveY(transform.position.y - distance * Screen.height* 0.5f, time * 0.2f).SetEase(Ease.OutElastic);
        graphic.DOFade(1, time * 0.2f).OnComplete(() => graphic.DOFade(1, time * 0.6f)
            .OnComplete(() => graphic.DOFade(0, time * 0.2f).OnComplete(() => gameObject.SetActive(false))));
    }
}
