using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRecorder : MonoBehaviour
{
    public string description;

    private void OnEnable()
    {
        DataRecord.GenerateCSVFile(GameManager.playerName + "_ScreenTime_" + GameManager.filePrefix, "\n" + description + "," + Time.time.ToString());
    }
}
