using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI modeText;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        titleText.text = $"Hello {GameManager.PlayerName},";
        modeText.text = GameManager.Current.ModeName;
    }

    public void LoadScene(int index)
    {
        GameManager.Current.Game = (GameManager.Game)index;
        SceneManager.LoadScene("RoundSelection " + GameManager.Current.Mode);
    }
}
