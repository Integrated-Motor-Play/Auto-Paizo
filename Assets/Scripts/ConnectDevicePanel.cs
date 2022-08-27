using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using General;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectDevicePanel : MonoBehaviour
{
    public static ConnectDevicePanel Instance;
    public DeviceDetailPanel deviceDetailPanel;
    public CalibrationController calibrationPanel;
    public PlayButtonHover playButtonHover;
    public GameObject p1H2, p2H4;
    public GameManager.Game[] p1H2Games, p2H4Games;
    
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        if(p1H2Games.Contains(GameManager.Current.Game))
            p1H2.SetActive(true);
        if(p2H4Games.Contains(GameManager.Current.Game))
            p2H4.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneName.GAMEPLAY_COMPUTER, LoadSceneMode.Additive);
    }
}
