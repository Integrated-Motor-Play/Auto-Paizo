using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public class NextSceneButton : MonoBehaviour
    {
        public void NextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
