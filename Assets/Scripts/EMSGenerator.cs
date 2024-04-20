using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class EMSGenerator : MonoBehaviour
{
    public bool isHotHands;
    public string[] EMSList;
    private string[] EMSList_3;
    private void Awake()
    {
        isHotHands = GameManager.Current.Game == GameManager.Game.SlapMeIfYouCan;
        print("Mode: " + GameManager.Current.GameName);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        EMSList_3 = new string[EMSList.Length];
        for (int i = 0; i < EMSList.Length; i++)
        {
            EMSList_3[i] = EMSList[i].Substring(0, 3);
        }
    }

    public void UpdatePlayList()
    {
        if (GameManager.Current.Round == GameManager.Round.BestOf3)
        {
            ModeManager.PlayList = EMSList_3[Random.Range(0, 10)];
        }
        else if (GameManager.Current.Round == GameManager.Round.BestOf5)
        {
            ModeManager.PlayList = EMSList[Random.Range(0, 10)];
        }
        else if (GameManager.Current.Round == GameManager.Round.FreePlay)
        {
            ModeManager.PlayList = string.Empty;
            for (int i = 0; i < 50; i++)
            {
                ModeManager.PlayList += Random.Range(0, 3).ToString();
            }
        }
        else if (ModeManager.CurrentMode.RoundMode == ModeManager.RoundMode.Walk)
        {
            for (int i = 0; i < 50; i++)
            {
                ModeManager.PlayList += Random.Range(0, 3).ToString();
            }
        }
        else if (ModeManager.CurrentMode.RoundMode == ModeManager.RoundMode.InfiniteLoop)
        {
            for (int i = 0; i < 50; i++)
            {
                ModeManager.PlayList += Random.Range(1, 4).ToString();
            }
        }

        if (isHotHands)
        {
            string temp = string.Empty;
            for (int i = 0; i < ModeManager.PlayList.Length; i++)
            {
                temp += Random.Range(0f, 1f) > 0.5f ? "1" : "3";
            }
            ModeManager.PlayList = temp;
        }
        DataRecord.GenerateCSVFile(GameManager.PlayerName + "_ScreenTime_" + GameManager.FilePrefix, ",Array: " + ModeManager.PlayList + "," + Time.time);
        print("Array: " + ModeManager.PlayList);
    }
}
