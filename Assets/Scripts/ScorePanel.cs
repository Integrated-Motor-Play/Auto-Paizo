using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePanel : MonoBehaviour
{
    public string which;
    public int score;
    public int actualScore;
    public TextMeshProUGUI scoreText;
    public void OnButtonClickAdd()
    {
        score++;
        actualScore += 2;
        UpdateAndGenerate();
    }

    public void OnButtonClickDraw()
    {
        score++;
        actualScore += 1;
        UpdateAndGenerate();

    }

    private void UpdateAndGenerate()
    {
        scoreText.text = score.ToString();
        DataRecord.GenerateCSVFile(GameManager.playerName + "_ScreenTime_" + GameManager.filePrefix, "," + which + ": " + scoreText.text + "," + Time.time);
        ScoreManager.Instance.ScoreChanged = true;
        if (actualScore > ModeManager.playList.Length ||
        (ScoreManager.Instance.EMSPanel.actualScore + ScoreManager.Instance.PlayerPanel.actualScore) >= 2 * ModeManager.playList.Length)
        {
            FindObjectOfType<HotHandsController>().GameOver();
        }
    }
}
