using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDevice : MonoBehaviour
{
    public void OnDropdownSelect(int Value)
    {
        GameManager.CurrentDevice = (GameManager.Device)Value;
        print("Set Device to: " + GameManager.CurrentDevice);
    }
}
