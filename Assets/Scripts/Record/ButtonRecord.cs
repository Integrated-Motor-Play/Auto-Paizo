using Managers;
using UnityEngine;

namespace Record
{
    public class ButtonRecord : MonoBehaviour
    {
        public void RecordButtonData(string buttonInfo)
        {
            DataRecord.GenerateCSVFile(GameManager.PlayerName + "_ScreenTime_" + GameManager.FilePrefix, "," + buttonInfo + "," + Time.time.ToString());
        }

    }
}
