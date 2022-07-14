using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI titleText;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        titleText.text = $"Hello {GameManager.PlayerName},";
    }

    public void LoadScene(int index)
    {
        GameManager.CurrentMode.GameMode = (GameManager.GameMode)index;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
