using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class FullScreenToggle : MonoBehaviour
    {
        public void SetFullscreen(bool isFullscreen)
        {
            //Full Screen Activate
            Screen.fullScreen = isFullscreen;
        }
    }
}