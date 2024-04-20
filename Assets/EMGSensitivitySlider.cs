using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EMGSensitivitySlider : MonoBehaviour
{
    public float Sensitivity { get; private set; }
    public Slider slider;
    public TextMeshProUGUI valueText;

    private void Awake()
    {
        Sensitivity = slider.value * 0.01f;
    }

    public void ChangeSensitivity(float value)
    {
        valueText.text = value.ToString("0") + "%";
        Sensitivity = value* 0.01f;
    }
}
