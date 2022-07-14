using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextPlayer : MonoBehaviour
{
    public enum textType
    {
        Game,
        Round,
    }
    public textType TextType;

    private void Update()
    {
        if (TextType == textType.Game)
            GetComponent<TextMeshProUGUI>().text = GameManager.CurrentMode.ModeName;
        if (TextType == textType.Round)
            GetComponent<TextMeshProUGUI>().text = ModeManager.CurrentMode.ModeName;
    }
}
