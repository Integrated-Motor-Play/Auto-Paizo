using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;

public class ModeButtonController : MonoBehaviour
{
    public GameManager.Round currentMode;
    public TextMeshProUGUI buttonText;
    public List<GameManager.Game> acceptedGames;

    private void Awake()
    {
        if (!acceptedGames.Contains(GameManager.Current.Game))
        {
            Destroy(gameObject);
        }
    }

    public void SwitchMode()
    {
        GameManager.Current.Round = currentMode;
        buttonText.text = GameManager.Current.RoundName;
        gameObject.name = GameManager.Current.RoundName;
    }

    private void OnValidate()
    {
        SwitchMode();
    }
}
