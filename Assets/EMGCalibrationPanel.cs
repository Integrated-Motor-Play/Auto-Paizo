using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EMGCalibrationPanel : MonoBehaviour
{
    public Window_Graph player1, player2;
    public Image timerFill;
    public TextMeshProUGUI remindText;
    public TextMeshProUGUI startButtonText;
    public bool Done { get; private set; }
    public ButtonHover playButtonHover;
    
    private float timer;
    private float timerAll;

    private List<float> emgAverageHigh1 = new List<float>(), emgAverageLow1 = new List<float>();
    private List<float> emgAverageHigh2 = new List<float>(), emgAverageLow2 = new List<float>();
    
    private void Awake()
    {
        var cells = FindObjectsOfType<EMSConnectCell>().ToList();
        var leftHand = cells.Find(c => c.hand == EMSConnectCell.Hand.LeftHand);
        var rightHand = cells.Find(c => c.hand == EMSConnectCell.Hand.RightHand);
        player1.Connector = leftHand.connector;
        player2.Connector = rightHand.connector;
    }

    private void OnEnable()
    {
        var tempText = "";
        tempText += GetCalibrationText(player1.Connector) + GetCalibrationText(player2.Connector);
        remindText.text = tempText;
    }

    private void Update()
    {
        timerFill.fillAmount = timer / timerAll;
        timer -= Time.deltaTime;
    }

    private IEnumerator StartCounting(List<float> list, Window_Graph player)
    {
        for (var i = 0; i < 100; i++)
        {
            list.Add(player.Connector.EmgValue);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator StartCalibration()
    {
        StartCoroutine(StartCounting(emgAverageLow1, player1));
        StartCoroutine(StartCounting(emgAverageLow2, player2));
        yield return TimerRound("Rest for 10 Seconds", 10);
        yield return TimerRound("...", 3);
        StartCoroutine(StartCounting(emgAverageHigh1, player1));
        StartCoroutine(StartCounting(emgAverageHigh2, player2));
        yield return TimerRound("Exert for 10 Seconds", 10);
        yield return TimerRound("...", 3);
        StartCoroutine(StartCounting(emgAverageHigh1, player1));
        StartCoroutine(StartCounting(emgAverageHigh2, player2));
        yield return TimerRound("Exert for 10 Seconds", 10);
        yield return TimerRound("...", 3);
        StartCoroutine(StartCounting(emgAverageLow1, player1));
        StartCoroutine(StartCounting(emgAverageLow2, player2));
        yield return TimerRound("Rest for 10 Seconds", 10);

        var lowAverage1 = emgAverageLow1.Average();
        var lowAverage2 = emgAverageLow2.Average();
        var highAverage1 = emgAverageHigh1.Average();
        var highAverage2 = emgAverageHigh2.Average();
        player1.Connector.LowBound = lowAverage1;
        player1.Connector.HighBound = highAverage1;
        player2.Connector.LowBound = lowAverage2;
        player2.Connector.HighBound = highAverage2;
        remindText.text = "All Done!\n" + GetCalibrationText(player1.Connector) + GetCalibrationText(player2.Connector);
        Done = true;
        playButtonHover.canReveal = true;
        playButtonHover.TryToRevealButton();
    }

    private IEnumerator TimerRound(string text, float time)
    {
        remindText.text = text;
        timer = time;
        timerAll = time;
        yield return new WaitForSeconds(time);
    }

    public void StartCalibrateClicked()
    {
        startButtonText.text = "Restart Calibration";
        emgAverageHigh1 .Clear(); emgAverageLow1 .Clear();
        emgAverageHigh2 .Clear(); emgAverageLow2 .Clear();

        StopAllCoroutines();
        StartCoroutine(StartCalibration());
    }

    private string GetCalibrationText( BluetoothConnector playerConnector)
    {
        if(playerConnector.BoundAssigned)
            return $"<size=36>{playerConnector.DeviceName}:\n" +
               $"Low Average: {playerConnector.LowBound:f2}, High Average:{playerConnector.HighBound:f2};</size>\n";
        return $"<size=36>{playerConnector.DeviceName}\n" +
               "Calibration not assigned</size>\n";
    }
}
