using UnityEngine;

public class EMGCalibrationGraph : MonoBehaviour
{
    public Window_Graph Graph;
    public CalibrationController Controller;

    private void Update()
    {
        if(Graph.Connector == null)
            Graph.Connector = Controller.connector;
    }
}
