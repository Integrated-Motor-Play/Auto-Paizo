using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class TimeRecorder : MonoBehaviour
{
    public string description;

    private void OnEnable()
    {
        DataRecord.GenerateCSVFile(GameManager.PlayerName + "_ScreenTime_" + GameManager.FilePrefix, "\n" + description + "," + Time.time.ToString());
    }
}
