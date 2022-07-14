using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;

public class ModeButtonController : MonoBehaviour
{
    public ModeManager.RoundMode currentMode;
    public TextMeshProUGUI buttonText;
    public bool slapMeIfYouCan, numbers, elements, blackJack, trangularMatch;

    private void Awake()
    {
        switch (GameManager.CurrentMode.GameMode)
        {
            case GameManager.GameMode.SlapMeIfYouCan:
                if(!slapMeIfYouCan) Destroy(gameObject);
                break;
            case GameManager.GameMode.Numbers:
                if(!numbers) Destroy(gameObject);
                break;
            case GameManager.GameMode.Elements:
                if(!elements) Destroy(gameObject);
                break;
            case GameManager.GameMode.TwentyOne:
                break;
            case GameManager.GameMode.BlackJack:
                if(!blackJack) Destroy(gameObject);
                break;
            case GameManager.GameMode.TriangularMatch:
                if(!trangularMatch) Destroy(gameObject);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SwitchMode()
    {
        ModeManager.CurrentMode.RoundMode = currentMode;
        buttonText.text = ModeManager.CurrentMode.ModeName;
        gameObject.name = ModeManager.CurrentMode.ModeName;
    }

    private void OnValidate()
    {
        SwitchMode();
    }
}
