using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGraph : MonoBehaviour
{
   public Window_Graph graph;
   public TextMeshProUGUI graphText;
   [SerializeField] private TextMeshProUGUI valueText;

   private void OnEnable()
   {
      if (graph.Connector != null)
      {
         graphText.text = graph.Connector.DeviceName;
         valueText.text = graph.Value.ToString("f2");
      }
   }
}
