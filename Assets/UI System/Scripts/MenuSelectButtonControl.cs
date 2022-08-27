using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystem
{
    public class MenuSelectButtonControl : MonoBehaviour
    {
        public GameObject FirstButton, CloseButton;

        private void OnEnable()
        {
            // clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            // set new
            StartCoroutine(SetFirstSelectable());
        }

        private void OnDisable()
        {
            if (CloseButton == null || EventSystem.current == null) return;
            // clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            // set new
            EventSystem.current.SetSelectedGameObject(CloseButton);
        }

        IEnumerator SetFirstSelectable()
        {
            yield return new WaitForEndOfFrame();
            EventSystem.current.SetSelectedGameObject(FirstButton);
        }
    }
}