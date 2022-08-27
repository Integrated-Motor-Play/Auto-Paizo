using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnimationFadeIn : MonoBehaviour
{
    public float startAlpha = 0;
    public float endAlpha = 0.8f;
    public float time = 0.3f;
    public Image image;
    public CanvasGroup canvasGroup;
    
    private void OnEnable()
    {
        if (image != null)
        {
            var color = image.color;
            color.a = startAlpha;
            image.color = color;
            image.DOKill();
            image.DOFade(endAlpha, time).SetUpdate(true);
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = startAlpha;
            canvasGroup.DOFade(endAlpha, time).SetUpdate(true);
        }
    }
}
