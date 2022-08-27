using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextPlayer : MonoBehaviour
{
    public string prefix;
    public string suffix;
    public enum textType
    {
        Game,
        Round,
        Mode,
    }
    public textType TextType;

    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = TextType switch
        {
            textType.Game => prefix + GameManager.Current.GameName + suffix,
            textType.Round => prefix + GameManager.Current.RoundName + suffix,
            textType.Mode => prefix + GameManager.Current.ModeName + suffix,
            _ => GetComponent<TextMeshProUGUI>().text
        };
    }
}
