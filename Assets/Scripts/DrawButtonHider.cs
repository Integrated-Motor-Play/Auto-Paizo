using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawButtonHider : MonoBehaviour
{
    private void OnEnable()
    {
        if (GameManager.currentMode != GameManager.GameMode.RockPaperScissors)
        {
            gameObject.SetActive(false);
        }
    }
}
