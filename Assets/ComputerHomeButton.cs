using System.Collections;
using System.Collections.Generic;
using General;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ComputerHomeButton : MonoBehaviour
{
    public void GoHome()
    {
        SceneManager.LoadScene(SceneName.MAIN_MENU);
        foreach (var cell in FindObjectsOfType<EMSConnectCell>())
        {
            cell.transform.SetParent(GameManager.Instance.transform);
        }
    }
}
