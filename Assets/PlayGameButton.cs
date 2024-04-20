using System.Collections;
using System.Collections.Generic;
using General;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameButton : MonoBehaviour
{
    public void StartGame()
    {
        var scene = GameManager.Current.Mode switch
        {
            GameManager.Mode.Social => SceneName.GAMEPLAY_SOCIAL,
            GameManager.Mode.Computer => SceneName.GAMEPLAY_COMPUTER,
            _ => "Not Found"
        };
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }
}
