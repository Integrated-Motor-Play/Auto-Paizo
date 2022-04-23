using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginCheck : MonoBehaviour
{
    public TMP_InputField nameField;
    public GameObject beginButton, checkMark;

    public void CheckNameWords(string name)
    {
        beginButton.SetActive(name.Length > 0);
        checkMark.SetActive(name.Length > 0);
    }

    public void OnClickStart()
    {
        PlayerPrefs.SetString("PlayerName", nameField.text);
        GameManager.playerName = nameField.text;
        DataRecord.GenerateCSVFile(GameManager.playerName + "_ScreenTime_" + GameManager.filePrefix, "Panel,Time");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
