using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class GamePlayPanel : MonoBehaviour
{
    public GameObject[] elementPanel, sevenPanel, matchPanel, match1Panel;

    private void OnEnable()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        switch (GameManager.Current.Game)
        {
            case GameManager.Game.Elements:
                foreach (var obj in elementPanel)
                {
                    obj.SetActive(true);
                }
                break;
            case GameManager.Game.BlackJack:
                foreach (var obj in sevenPanel)
                {
                    obj.SetActive(true);
                }
                break;
            case GameManager.Game.Match2Players:
                foreach (var obj in matchPanel)
                {
                    obj.SetActive(true);
                }
                break;
            case GameManager.Game.SlapMeIfYouCan:
                break;
            case GameManager.Game.Numbers:
                break;
            case GameManager.Game.TwentyOne:
                break;
            case GameManager.Game.Match1Player:
                foreach (var obj in match1Panel)
                {
                    obj.SetActive(true);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDisable()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }
}
