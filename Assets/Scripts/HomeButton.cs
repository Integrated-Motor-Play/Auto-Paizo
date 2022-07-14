using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        CalibrationController.SendBluetoothData("r");
    }
}
