
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gesture", menuName = "Gesture")]
public class Gesture : ScriptableObject
{
    public string gestureName;
    public int channel;
    public Sprite icon;
}
