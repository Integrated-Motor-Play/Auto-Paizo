using System;
using General;
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

        public void OnClickStartSinglePlayer()
        {
            SetUp();
            GameManager.Current.Mode = GameManager.Mode.SinglePlayer;
            SceneManager.LoadScene(SceneName.BLUETOOTH_SINGLE);
        }
        
        public void OnClickStartSocial()
        {
            SetUp();
            GameManager.Current.Mode = GameManager.Mode.Social;
            SceneManager.LoadScene(SceneName.MAIN_MENU);
        }
        
        public void OnClickStartComputer()
        {
            SetUp();
            GameManager.Current.Mode = GameManager.Mode.Computer;
            SceneManager.LoadScene(SceneName.MAIN_MENU);
        }

        private void SetUp()
        {
            PlayerPrefs.SetString("PlayerName", nameField.text);
            GameManager.PlayerName = nameField.text;
            DataRecord.GenerateCSVFile(GameManager.PlayerName + "_ScreenTime_" + GameManager.FilePrefix, "Panel,Time");
        }

        public void UpdateTitleText(string playerName)
        {
            if (playerName.IsNullOrEmpty())
                title.text = "Welcome to EMS Games!";
            else
                title.text = "Hi " + playerName + "!";
        }
    }
}
