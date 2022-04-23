using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementSave : MonoBehaviour
{
    [TextArea]
    public string Text;
    public int Points;
    public int Index;
    public TextMeshProUGUI AchievementText;
    public Toggle toggle;

    private string label;
    private void Awake()
    {
        AchievementText.text = Text;

        label = GameManager.playerName + "_Achievement_" + Index.ToString();
        if (PlayerPrefs.GetInt(label, 0) == 1)
            toggle.isOn = true;
        else toggle.isOn = false;
    }

    public void AchievementUpdate(bool toggle)
    {
        if (toggle)
            PlayerPrefs.SetInt(label, 1);
        else
        {
            PlayerPrefs.SetInt(label, 0);
        }
    }

    private void OnValidate()
    {
        AchievementText.text = Text;
    }
}
