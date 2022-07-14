using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class DrawButtonHider : MonoBehaviour
{
    private void OnEnable()
    {
        if (GameManager.CurrentMode.GameMode != GameManager.GameMode.Elements)
        {
            gameObject.SetActive(false);
        }
    }
}
