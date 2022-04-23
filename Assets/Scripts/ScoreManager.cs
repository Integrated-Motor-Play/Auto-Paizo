using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public ScorePanel EMSPanel, PlayerPanel;
    [HideInInspector]
    public bool ScoreChanged = true;
    public int CheckIfPlayerWin()
    {
        if (PlayerPanel.score > EMSPanel.score) { return 1; }
        else if (PlayerPanel.score == EMSPanel.score) { return 0; }
        else { return -1; }
    }

    public void ResetScore()
    {
        PlayerPanel.score = 0;
        EMSPanel.score = 0;
        PlayerPanel.actualScore = 0;
        EMSPanel.actualScore = 0;
        PlayerPanel.scoreText.text = "0";
        EMSPanel.scoreText.text = "0";
    }
}
