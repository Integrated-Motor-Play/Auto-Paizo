using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestureElement : MonoBehaviour
{
    public bool IsEliminated { get; private set; }

    public Gesture gesture;
    
    public Image bg;
    public TextMeshProUGUI gestureNameText;
    public Image icon;
    public RectTransform rectTransform;

    private static Transform _parent;

    private void Awake()
    {
        _parent = transform.parent;
    }

    private void OnValidate()
    {
        //ArrangeColor();
        //UpdatePosition();
        if(gesture == null) return;
        gestureNameText.text = gesture.gestureName;
        icon.sprite = gesture.icon;
    }

    public void Eliminate()
    {
        IsEliminated = true;
        var eliminatedCount = GetEliminatedCount();
        //print("eliminatedCount: " + eliminatedCount);
        transform.SetSiblingIndex(transform.parent.childCount - eliminatedCount);
        gestureNameText.DOColor(Color.gray, 0.5f);
        gestureNameText.fontStyle = FontStyles.Strikethrough;
        icon.DOFade(0.2f, 0.5f);
    }

    public static int GetEliminatedCount()
    {
        return _parent.GetComponentsInChildren<GestureElement>().ToList()
            .FindAll(e => e.IsEliminated).Count;
    }

    private void Update()
    {
        ArrangeColor();
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        var tempPos = rectTransform.anchoredPosition;
        var offSet = (Vector3)tempPos - rectTransform.position;        
        var height = rectTransform.rect.height;
        tempPos.y = -height * 0.5f - height * transform.GetSiblingIndex();
        if(rectTransform.anchoredPosition != tempPos && !DOTween.IsTweening(rectTransform))
            rectTransform.DOMove((Vector3) tempPos - offSet, 0.5f);
    }

    private void ArrangeColor()
    {
        var alpha = transform.GetSiblingIndex() % 2 == 0 ? 0:10;
        if (!DOTween.IsTweening(bg))
            bg.DOFade(alpha / 255f, 0.5f);
    }
}
