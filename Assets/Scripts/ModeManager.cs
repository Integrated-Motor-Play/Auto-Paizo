using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public enum GameMode
    {
        Bestof3 = 0,
        Bestof5 = 1,
        FreePlay = 2,

    }
    public static GameMode currentMode = GameMode.FreePlay;
    public static string playList;
}
