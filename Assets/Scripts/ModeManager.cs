using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    public enum RoundMode
    {
        BestOf3 = 0,
        BestOf5 = 1,
        FreePlay = 2,
        Walk = 3,
        InfiniteLoop = 4
    }
    public static Mode CurrentMode;
    public static string PlayList;
    
    public struct Mode
    {
        public RoundMode RoundMode;

        public string ModeName
        {
            get
            {
                var name = RoundMode switch
                {
                    RoundMode.BestOf3 => "Best of 3",
                    RoundMode.BestOf5 => "Best of 5",
                    RoundMode.FreePlay => "Free Play",
                    RoundMode.Walk => "Walk",
                    RoundMode.InfiniteLoop => "Infinite Loop",
                    _ => throw new ArgumentOutOfRangeException()
                };
                return name;
            }
        }
    }
}
