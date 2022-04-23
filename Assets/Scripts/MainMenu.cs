using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI titleText;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        titleText.text = string.Format("Hello {0},", GameManager.playerName);
    }

    public void LoadScene(int index)
    {
        GameManager.currentMode = (GameManager.GameMode)index;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
