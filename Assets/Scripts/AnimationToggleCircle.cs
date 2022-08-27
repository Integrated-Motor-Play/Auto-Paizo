using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnimationToggleCircle : MonoBehaviour
{
    private Image _image;
    public float fillTime = 0.3f;
    public Transform innerIcon;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void PlayAnimation(bool on)
    {
        if(!on) return;
        DOTween.Kill(_image);
        _image.fillAmount = 0;
        _image.DOFillAmount(1, fillTime);

        innerIcon.DORotate(Vector3.forward * 20, fillTime *0.25f).OnComplete(
           () => innerIcon.DORotate(Vector3.back * 20, fillTime *0.5f).OnComplete(
               () => innerIcon.DORotate(Vector3.zero, fillTime *0.25f)
               )
            );
    }
}
