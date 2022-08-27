using System;
using Managers;
using UnityEngine;

public class ModeFilter:MonoBehaviour
{
    public GameManager.Mode thisMode;
    private void OnEnable()
    {
        gameObject.SetActive(thisMode == GameManager.Current.Mode);

    }
}