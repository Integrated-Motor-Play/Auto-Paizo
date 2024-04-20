
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gesture", menuName = "Gesture")]
public class Gesture : ScriptableObject
{
    [PreviewField(70),HideLabel]
    [HorizontalGroup("Split",70)]
    public Sprite icon;
    [VerticalGroup("Split/Right"), LabelWidth(120)]
    [Title("$gestureName")]
    public string gestureName;
    [VerticalGroup("Split/Right"), LabelWidth(120)]
    public int channel;
}
