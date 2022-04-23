using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
