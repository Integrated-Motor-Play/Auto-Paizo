using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberBox : MonoBehaviour
{
    public Color color;
    
    [SerializeField]
    private int? number;

    [SerializeField]
    private TextMeshProUGUI numberText;

    [SerializeField] private Image image;

    private void OnValidate()
    {
        UpdateImage();
        UpdateText();
    }

    private void UpdateImage()
    {
        if(image != null)
            image.color = color;
    }

    private void UpdateText()
    {
        if (numberText == null) return;

        numberText.text = number == null ? string.Empty : number.ToString();
    }

    public void SetNumber(int? num)
    {
        number = num;
        UpdateText();
    }
}
