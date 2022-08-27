using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class ChooseDevice : MonoBehaviour
{
    public Image deviceImage;
    public Sprite seeeduino, adafruit;
    
    public void OnDropdownSelect(int value)
    {
        GameManager.Current.Device = (GameManager.Device)value;
        print("Set Device to: " + GameManager.Current.Device);
        deviceImage.sprite = value switch
        {
            0 => seeeduino,
            1 => adafruit,
            _ => deviceImage.sprite
        };
    }
}
