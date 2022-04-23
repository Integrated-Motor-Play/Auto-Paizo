using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public TextMeshProUGUI WinMessage, ScoreMessage;
    public AudioClip[] Sound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        switch (ScoreManager.Instance.CheckIfPlayerWin())
        {
            case -1: PlaySound(0); WinMessage.text = "Player 1 Win!"; break;
            case 0: PlaySound(1); WinMessage.text = "Draw!"; break;
            case 1: PlaySound(2); WinMessage.text = "Player 2 Win!"; break;
            default: break;
        }

        ScoreMessage.text = ScoreManager.Instance.PlayerPanel.score.ToString() + ":" + ScoreManager.Instance.EMSPanel.score.ToString();
    }

    public void PlaySound(int index)
    {
        audioSource.clip = Sound[index];
        audioSource.Play();
    }
}
