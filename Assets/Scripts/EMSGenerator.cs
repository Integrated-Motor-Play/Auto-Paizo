using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMSGenerator : MonoBehaviour
{
    public bool isHotHands;
    public string[] EMSList;
    private string[] EMSList_3;
    private void Awake()
    {
        isHotHands = GameManager.currentMode == GameManager.GameMode.HotHands;
        print("Mode: " + GameManager.currentMode);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        EMSList_3 = new string[EMSList.Length];
        for (int i = 0; i < EMSList.Length; i++)
        {
            EMSList_3[i] = EMSList[i].Substring(0, 3);
        }
    }

    public void UpdatePlayList()
    {
        if (ModeManager.currentMode == ModeManager.GameMode.Bestof3)
        {
            ModeManager.playList = EMSList_3[Random.Range(0, 10)];
        }
        else if (ModeManager.currentMode == ModeManager.GameMode.Bestof5)
        {
            ModeManager.playList = EMSList[Random.Range(0, 10)];
        }
        else if (ModeManager.currentMode == ModeManager.GameMode.FreePlay)
        {
            ModeManager.playList = string.Empty;
            for (int i = 0; i < 50; i++)
            {
                ModeManager.playList += Random.Range(0, 3).ToString();
            }
        }
        else if (ModeManager.currentMode == ModeManager.GameMode.Walk)
        {
            for (int i = 0; i < 50; i++)
            {
                ModeManager.playList += Random.Range(0, 3).ToString();
            }
        }

        if (isHotHands)
        {
            string temp = string.Empty;
            for (int i = 0; i < ModeManager.playList.Length; i++)
            {
                temp += Random.Range(0f, 1f) > 0.5f ? "1" : "3";
            }
            ModeManager.playList = temp;
        }
        DataRecord.GenerateCSVFile(GameManager.playerName + "_ScreenTime_" + GameManager.filePrefix, ",Array: " + ModeManager.playList + "," + Time.time);
        print("Array: " + ModeManager.playList);
    }
}
