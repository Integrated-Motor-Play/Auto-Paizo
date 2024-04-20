using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class PlantControl : MonoBehaviour
{
    public Window_Graph playerGraph;
    public CalibrationController calibrationController;
    private Color circleColor;
    public Color highlightCircleColor;
    public Color minColor, maxColor;
    public TextMeshProUGUI graphText;
    public BluetoothConnector connector;
    private bool _active;

    private bool _running;
    
    public float minGraphValue { get; set; }   
    public float maxGraphValue { get; set; }

    private float _lowBound, _highBound;
    private float _minValue, _maxValue;

    public void SetLowBound(string bound)
    {
        if (bound.IsNullOrEmpty()) return;
        minGraphValue = float.Parse(bound);
        UpdateValues();
    }
    public void SetHighBound(string bound)
    {
        if (bound.IsNullOrEmpty()) return;
        maxGraphValue = float.Parse(bound);
        UpdateValues();
    }

    private void UpdateValues()
    {
        DrawTriggerMin(_minValue.ToString());
        DrawTriggerMax(_maxValue.ToString());
    }
    
    public void StartClicked()
    {
        playerGraph.Connector = connector;
        calibrationController.SetupCalibration(connector, "Device");
        _running = true;
    }
    
    public void StopClicked()
    {
        playerGraph.Connector = null;
        _running = false;
        calibrationController.Channel1(false);
        _active = false;
    }

    private void Awake()
    {
        circleColor = playerGraph.circleColor ;
    }

    private void Start()
    {
        UpdateValues();
    }

    private void Update()
    {
        if (_running)
        {
            graphText.text = connector.EmgValue.ToString("0");
            var emgValue = playerGraph.Connector.EMGValueNormalized;
            if (emgValue < _highBound && emgValue > _lowBound)
            {
                playerGraph.circleColor = highlightCircleColor;
                if (!_active)
                {
                    calibrationController.Channel1(true);
                    _active = true;
                }
            }
            else
            {
                playerGraph.circleColor = circleColor;
                if (_active)
                {
                    _active = false;
                    calibrationController.Channel1(false);
                }
            }
        }
    }

    public void DrawTriggerMin(string value)
    {
        if (value.IsNullOrEmpty()) return;
        _minValue = float.Parse(value);
        _lowBound = Normalize(_minValue);
        playerGraph.DrawUniqueLine(_lowBound,minColor, "min");
    }
    
    public void DrawTriggerMax(string value)
    {
        if (value.IsNullOrEmpty()) return;
        _maxValue = float.Parse(value);
        _highBound = Normalize(_maxValue);
        playerGraph.DrawUniqueLine(_highBound,maxColor, "max");
    }

    private float Normalize(float value)
    {
        return (value - minGraphValue) / (maxGraphValue - minGraphValue);
    }
}
