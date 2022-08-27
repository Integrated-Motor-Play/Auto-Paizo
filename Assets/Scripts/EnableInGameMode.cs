using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class EnableInGameMode : MonoBehaviour
{
    public GameManager.Game[] Modes;

    private void Awake()
    {
        bool canEnable = false;
        foreach (var mode in Modes)
        {
            if (GameManager.Current.Game == mode)
                canEnable = true;
        }
        gameObject.SetActive(canEnable);
    }
}
