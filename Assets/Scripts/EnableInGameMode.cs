using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableInGameMode : MonoBehaviour
{
    public GameManager.GameMode[] Modes;

    private void Awake()
    {
        bool canEnable = false;
        foreach (var mode in Modes)
        {
            if (GameManager.currentMode == mode)
                canEnable = true;
        }
        gameObject.SetActive(canEnable);
    }
}
