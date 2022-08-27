using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementPanel : GamePanel
{
    [SerializeField] private Sprite[] elements;
    [SerializeField] private GameObject elementImageHolder;
    [SerializeField] private Image elementImage, elementBarImage, elementBarBeatImage1, elementBarBeatImage2;

    public ElementsComputer.ElementInfo Element;

    private void OnEnable()
    {
        UpdateElementImage();
    }

    public override void ResetEverything()
    {
        Element = null;
        elementImageHolder.SetActive(false);
    }

    public void UpdateElementImage()
    {
        if (Element != null){
            elementImageHolder.SetActive(true);
            elementImage.sprite = elements[(int) Element.Element];
            elementBarImage.sprite = elements[(int) Element.Element];
            elementBarBeatImage1.sprite = elements[(int) Element.BeatElements[0]];
            elementBarBeatImage2.sprite = elements[(int) Element.BeatElements[1]];
        }
        else
            elementImageHolder.SetActive(false);
    }
}
