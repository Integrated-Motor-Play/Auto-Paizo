using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArmSparkPanel : MonoBehaviour, IEmsController
{
    public Window_Graph graph1, graph2;
    public EMGSensitivitySlider sensitivitySlider;
    private float _differChangeRate;

    public Image redBar;

    private float _lastDiffer = 10000f;
    private float _redFill = 0.5f;
    
    private void Awake()
    {
        var cells = FindObjectsOfType<EMSConnectCell>().ToList();
        var leftHand = cells.Find(c => c.hand == EMSConnectCell.Hand.LeftHand);
        var rightHand = cells.Find(c => c.hand == EMSConnectCell.Hand.RightHand);

        if(leftHand != null)
            graph1.Connector = leftHand.connector;
        if(rightHand != null)
            graph2.Connector = rightHand.connector;

        if(leftHand != null && rightHand != null) 
            StartCoroutine(CheckEmgChange());
    }

    public void TurnOffAllEms()
    {
        graph1.Connector.SendBluetoothData("r");
        graph2.Connector.SendBluetoothData("r");
    }

    IEnumerator CheckEmgChange()
    {
        while (true)
        {
            var gate = sensitivitySlider.Sensitivity;
            var differ = graph1.Connector.EMGValueNormalized - graph2.Connector.EMGValueNormalized;
        
            if (_lastDiffer != 10000f)
            {
                var differChange = differ - _lastDiffer;
                if (differChange > gate)
                {
                    // 1 strong
                    yield return ActiveEMS(graph2.Connector);
                }else if (-differChange > gate)
                {
                    // 2 strong
                    yield return ActiveEMS(graph1.Connector);
                }

                _differChangeRate = differChange / differ;
                print("differ: " + differ.ToString("f2") + ", dC: " + differChange.ToString("f2") + ", dCR: " + _differChangeRate);
            }
            differ = graph1.Connector.EMGValueNormalized - graph2.Connector.EMGValueNormalized;
            _lastDiffer = differ;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ActiveEMS(BluetoothConnector connector)
    {
        connector.SendBluetoothData("A");
        yield return new WaitForSeconds(2);
        connector.SendBluetoothData("a");
        // Cooldown
        yield return new WaitForSeconds(10);
    }
    
    private void Update()
    {
        var targetRate = _differChangeRate * 0.5f + 0.5f;
        _redFill += (targetRate - _redFill) * 0.1f;
        redBar.fillAmount = targetRate;
    }
}
