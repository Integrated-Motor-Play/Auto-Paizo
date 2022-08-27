using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ToggleOffGraphic : MonoBehaviour
{
    private Toggle _toggle;
    public Image offGraphic;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(OnValueChanged);
    }

    public void OnValueChanged(bool isOn)
    {
        var alpha = isOn ? 0 : 1;
        offGraphic.DOFade(alpha, 0.2f);
    }
}
