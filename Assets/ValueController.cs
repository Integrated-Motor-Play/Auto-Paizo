using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValueController : MonoBehaviour
{
    public TMP_InputField value;

    public string prefKey = "minGraphValue";

    private void Start()
    {
        var myValue = PlayerPrefs.GetFloat(prefKey, float.Parse(value.text));
        value.text = myValue.ToString("f1");
        value.onValueChanged.Invoke(value.text);
        value.onValueChanged.AddListener(s => SaveValue(float.Parse(s)));
    }

    private void SaveValue(float targetValue)
    {
        PlayerPrefs.SetFloat(prefKey, targetValue);
    }
}
