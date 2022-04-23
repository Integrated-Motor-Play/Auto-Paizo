using UnityEngine;

public class ButtonRecord : MonoBehaviour
{
    public void RecordButtonData(string buttonInfo)
    {
        DataRecord.GenerateCSVFile(GameManager.playerName + "_ScreenTime_" + GameManager.filePrefix, "," + buttonInfo + "," + Time.time.ToString());
    }

}
