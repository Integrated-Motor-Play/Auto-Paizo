using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class DebugToggle : MonoBehaviour
{
   private Toggle _toggle;

   private void Awake()
   {
      _toggle = GetComponent<Toggle>();
      var consoleToGUI = FindObjectOfType<ConsoleToGUI>();
      _toggle.onValueChanged.AddListener(consoleToGUI.SetDebugPanelShown);
      _toggle.isOn = consoleToGUI.debugLog.IsActive();
   }
}
