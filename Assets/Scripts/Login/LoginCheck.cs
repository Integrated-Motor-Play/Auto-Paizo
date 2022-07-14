using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;

namespace Login
{
    public class LoginCheck : MonoBehaviour
    {
        public TMP_InputField nameField;
        public TextMeshProUGUI title;

        private void OnEnable()
        {
            if (!GameManager.PlayerName.IsNullOrEmpty())
                nameField.text = GameManager.PlayerName;
            UpdateTitleText(GameManager.PlayerName);
        }

        public void OnClickStart()
        {
            PlayerPrefs.SetString("PlayerName", nameField.text);
            GameManager.PlayerName = nameField.text;
            DataRecord.GenerateCSVFile(GameManager.PlayerName + "_ScreenTime_" + GameManager.FilePrefix, "Panel,Time");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void UpdateTitleText(string playerName)
        {
            if (playerName.IsNullOrEmpty())
                title.text = "Welcome to Auto-Paizo!";
            else
                title.text = "Hi " + playerName + "!";
        }
    }
}
