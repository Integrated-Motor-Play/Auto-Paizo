using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UISystem
{
    [RequireComponent(typeof(Button))]
    public class ButtonSound : MonoBehaviour
    {
        private void Awake()
        {
            var btn = GetComponent<UnityEngine.UI.Button>();
            btn.onClick.AddListener(() => ButtonCallBack(btn));
        }

        private void ButtonCallBack(Button btn)
        {
            PlayButtonSound();
        }

        public void PlayButtonSound()
        {
            AudioManager.Instance.SFX.Play("ui_button_click");
        }
    }
}