using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextPlayer : MonoBehaviour
{
    public enum textType
    {
        Game,
        Round,
    }
    public textType TextType;
    [TextArea]
    public string[] text;


    private void Update()
    {
        if (TextType == textType.Game)
            GetComponent<TextMeshProUGUI>().text = text[(int)GameManager.currentMode];
        if (TextType == textType.Round)
            GetComponent<TextMeshProUGUI>().text = text[(int)ModeManager.currentMode];
    }
}
