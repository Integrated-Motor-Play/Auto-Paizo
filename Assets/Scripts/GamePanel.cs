using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GamePanel : MonoBehaviour
{
    public EMSConnectCell.Hand hand;
    public string title = "Title";
    public Color color = Color.white;
    [SerializeField] protected Graphic[] coloredGraphics;
    [SerializeField] protected TextMeshProUGUI titleText;
    
    protected virtual void UpdateColors()
    {
        foreach (var graphic in coloredGraphics)
        {
            graphic.color = color;
        }
    }

    protected virtual void OnValidate()
    {
        titleText.text = title;
        UpdateColors();
    }
    
    public virtual void ResetEverything(){}
}
