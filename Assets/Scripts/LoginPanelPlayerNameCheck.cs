using UnityEngine.SceneManagement;
using UnityEngine;

public class LoginPanelPlayerNameCheck : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.playerName != string.Empty)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
