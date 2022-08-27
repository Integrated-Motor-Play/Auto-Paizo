using UnityEngine.Audio;
using UnityEngine;
using TMPro;

namespace UISystem
{
    public class VolumeSliderController : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public TextMeshProUGUI volumeAmount;
        public UnityEngine.UI.Slider slider;

        private void OnEnable()
        {
            var value = PlayerPrefs.GetFloat(audioMixer.name, 0f);
            //print("Get Volume: " + audioMixer.name + "  " + value);
            SetVolumeAmount(value);
            slider.value = value;
        }

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("volume", volume);
            PlayerPrefs.SetFloat(audioMixer.name, volume);
            print("Set Volume: " + audioMixer.name + "  " + volume);
            SetVolumeAmount(volume);
            if (audioMixer.name == "SFXMixer")
                AudioManager.Instance.SFX.Play("ui_options_sfx");
        }

        public void Mute(bool mute)
        {
            if (mute)
                audioMixer.SetFloat("volume", -80);
            else
                audioMixer.SetFloat("volume", PlayerPrefs.GetFloat(audioMixer.name, 0f));
        }

        private void SetVolumeAmount(float volume)
        {
            volumeAmount.text = ((volume + 80) * 100 / 80).ToString("0");
        }
    }
}
