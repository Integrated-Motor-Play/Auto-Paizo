using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeButtonController : MonoBehaviour
{
    public void SwitchMode(int modeIndex)
    {
        ModeManager.currentMode = (ModeManager.GameMode)modeIndex;
    }
}
