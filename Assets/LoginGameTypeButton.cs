using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;

public class LoginGameTypeButton : MonoBehaviour
{
   public GameManager.Mode gameType;
   public TextMeshProUGUI buttonName;

   private void OnValidate()
   {
      var gaming = new GameManager.Gaming
      {
         Mode = gameType
      };
      buttonName.text = gaming.ModeName;
   }
}
