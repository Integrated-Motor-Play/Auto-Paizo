using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Login
{
    public class LoginPanelPlayerNameCheck : MonoBehaviour
    {
        private void Start()
        {
            if (GameManager.PlayerName != string.Empty)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
