using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UISystem;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    private int _leftScore;
    private int _rightScore;
    private int _leftLocalScore, _rightLocalScore;

    public TextMeshProUGUI roundNumberText;
    public TextMeshProUGUI leftScoreText, rightScoreText;
    public Image topRedBar;
    public AnimationZoomIn leftFist, rightFist;
    [Header("Score Boards")]
    public Image getScoreBoard;
    public Image getScoreEmoji;
    public TextMeshProUGUI getScoreText;
    public Image finalScoreBoard;
    public Image finalScoreEmoji;
    public TextMeshProUGUI finalScoreText;
    
    public Sprite blueWinSprite, redWinSprite, drawSprite;

    private Color _leftHandColor, _rightHandColor;

    public void PlayAnimation(EMSConnectCell.Hand hand)
    {
        switch (hand)
        {
            case EMSConnectCell.Hand.LeftHand:
                leftFist.PlayAnimation();
                break;
            case EMSConnectCell.Hand.RightHand:
                rightFist.PlayAnimation();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(hand), hand, null);
        }
    }
    
    public void SetColor(EMSConnectCell.Hand hand, Color color)
    {
        switch (hand)
        {
            case EMSConnectCell.Hand.LeftHand:
                _leftHandColor = color;
                break;
            case EMSConnectCell.Hand.RightHand:
                _rightHandColor = color;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(hand), hand, null);
        }
    }
    
    public void LeftGetsPoint(string message)
    {
        print("Left Gets Point!");
        _leftScore++;
        leftScoreText.text = _leftScore.ToString();
        _leftLocalScore += 2;
        SetGetScoreBoard(_leftHandColor, message + "\n<size=70>Left Hand Gets the Point!</size>", redWinSprite,"computer_red_win");
    }

    public void RightGetsPoint(string message)
    {
        print("Right Gets Point!");
        _rightScore++;
        rightScoreText.text = _rightScore.ToString();
        _rightLocalScore += 2;
        SetGetScoreBoard(_rightHandColor, message + "\n<size=70>Right Hand Gets the Point!</size>", blueWinSprite,"computer_blue_win");
    }

    public void NoOneGetsPoint(string message)
    {
        print("No one Gets Point!");
        _leftLocalScore++;
        _rightLocalScore++;
        SetGetScoreBoard(Color.gray, message + "\nDraw!", drawSprite,"computer_draw");
    }

    public bool CheckIfOneWins()
    {
        return (Mathf.Max(_leftLocalScore, _rightLocalScore) > GameManager.Current.RoundValue
                || _leftLocalScore + _rightLocalScore > 2 * GameManager.Current.RoundValue);
    }

    private void Update()
    {
        UpdateTopBar();
    }

    private void UpdateTopBar()
    {
        var targetData = 0f;
        if (_leftScore == _rightScore)
        {
            targetData = 0.5f;
        }
        else
        {
            targetData = _leftScore / (float)(_leftScore + _rightScore);
        }

        topRedBar.fillAmount += Time.deltaTime * (targetData - topRedBar.fillAmount);
    }

    private void SetGetScoreBoard(Color color, string text, Sprite emoji, string sound)
    {
        getScoreBoard.gameObject.SetActive(true);
        getScoreBoard.color = color;
        getScoreEmoji.sprite = emoji;
        getScoreText.text = text;
        AudioManager.Instance.SFX.Play(sound, 1, 1);
    }

    public void RevealFinalScoreBoard()
    {
        var largerOne = Mathf.Max(_leftScore, _rightScore);
        var score = _leftScore + " : " + _rightScore + "\n";
        if (_leftScore == _rightScore)
        {
            ShowFinalScoreBoard(Color.white, score + "Draw", drawSprite);
            AudioManager.Instance.SFX.Play("computer_draw", 1, 1);
        }
        else if (_leftScore == largerOne)
        {
            AudioManager.Instance.SFX.Play("computer_red_win", 1, 1);
            ShowFinalScoreBoard(_leftHandColor, score + "Left Hand Wins the Game!", redWinSprite);
        }
        else if (_rightScore == largerOne)
        {
            AudioManager.Instance.SFX.Play("computer_blue_win", 1, 1);
            ShowFinalScoreBoard(_rightHandColor, score + "Right Hand Wins the Game!", blueWinSprite);
        }
    }

    public IEnumerator SetRoundText(int round)
    {
        roundNumberText.text = "3";
        yield return new WaitForSeconds(1);
        roundNumberText.text = "2";
        yield return new WaitForSeconds(1);
        roundNumberText.text = "1";
        yield return new WaitForSeconds(1);
        roundNumberText.text = "Round " + round;
    }

    private void ShowFinalScoreBoard(Color color, string text, Sprite emoji)
    {
        finalScoreBoard.gameObject.SetActive(true);
        finalScoreBoard.color = color;
        finalScoreText.text = text;
        finalScoreEmoji.sprite = emoji;
    }
}
