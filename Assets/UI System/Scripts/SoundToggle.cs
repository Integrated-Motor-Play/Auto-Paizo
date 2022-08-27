using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UISystem
{
    public class SoundToggle : MonoBehaviour
    {
        public VolumeSliderController MusicBarGroup, SFXBarGroup;
        public Slider MusicBar, SFXBar;


        public void ToggleSound(bool on)
        {
            MusicBar.enabled = on;
            SFXBar.enabled = on;
            MusicBarGroup.GetComponent<CanvasGroup>().alpha = on ? 1 : 0.5f;
            SFXBarGroup.GetComponent<CanvasGroup>().alpha = on ? 1 : 0.5f;
            MusicBarGroup.Mute(!on);
            SFXBarGroup.Mute(!on);
        }
    }
}
