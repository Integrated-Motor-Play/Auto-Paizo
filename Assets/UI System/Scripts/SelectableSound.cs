using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystem
{
    public class SelectableSound : MonoBehaviour
    {
        public bool IsSelected;
        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                if (!IsSelected)
                {
                    IsSelected = true;
                    AudioManager.Instance.SFX.Play("ui_button_hover");
                }
            }
            else
            {
                IsSelected = false;
            }
        }
    }
}