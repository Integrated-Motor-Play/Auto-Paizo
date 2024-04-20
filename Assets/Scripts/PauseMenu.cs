using DG.Tweening;
using General;
using Managers;
using UISystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public IEmsController EmsController;
    public GameObject pausePanel;

    private void Awake()
    {
        Instance = this;
    }

    public void Pause()
    {
        if (pausePanel.activeSelf) return;
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        EmsController?.TurnOffAllEms();
        AudioManager.Instance.SFX.Play("pause");
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        var scene = GameManager.Current.Mode switch
        {
            GameManager.Mode.Social => SceneName.GAMEPLAY_SOCIAL,
            GameManager.Mode.Computer => SceneName.GAMEPLAY_COMPUTER,
            _ => "Not Found"
        };
        SceneManager.UnloadSceneAsync(scene);
        //SceneManager.LoadScene(SceneName.GAMEPLAY_COMPUTER, LoadSceneMode.Additive);
    }

    private void OnDestroy()
    {
        Resume();
        DOTween.KillAll();
    }
}
