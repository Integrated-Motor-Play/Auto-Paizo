using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystem
{
    public class PauseMenu : MonoBehaviour
    {
        public static PauseMenu Instance;
        public GameObject ButtonPanel, HoverPanel;
        [Header("Buttons")]
        public GameObject FirstButton;

        public bool IsPaused;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsPaused)
                {
                    Continue();
                }
                else
                {
                    Pause();
                    ButtonPanel.SetActive(true);
                }
            }
        }

        private void Pause()
        {
            Time.timeScale = 0;
            IsPaused = true;
            AudioManager.Instance.SFX.Play("ui_on");
            HoverPanel.SetActive(true);

            // clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            // set new
            StartCoroutine(SetFirstSelectable());
        }

        public void Continue()
        {
            Time.timeScale = 1;
            IsPaused = false;
            AudioManager.Instance.SFX.Play("ui_off");
            foreach (var panel in GetComponentsInChildren<PauseMenuPanel>())
            {
                panel.gameObject.SetActive(false);
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        IEnumerator SetFirstSelectable()
        {
            yield return new WaitForEndOfFrame();
            EventSystem.current.SetSelectedGameObject(FirstButton);
        }
    }
}