using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadNextScene : MonoBehaviour
{
    // Time delay before loading the next scene
    public float delay = 3f;

    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        // Load the next scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}