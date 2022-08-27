using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BreathingPanel : MonoBehaviour
{
    public float breathTime = 20;
    public float fadeTime = 1;

    public TextMeshProUGUI breathText, timeCountText;
    public Image timeCountCircle;
    public GameObject gamePanel;
    public CanvasGroup skipButton;

    private CanvasGroup _canvasGroup;
    private float timeTemp;
    private bool gameStarted;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        
        skipButton.GetComponent<Button>().onClick.AddListener(Skip);
        
        timeCountText.text = string.Empty;
        var tempColor = timeCountText.color;
        tempColor.a = 0;
        timeCountText.color = tempColor;
    }

    private void Skip()
    {
        timeTemp = breathTime - 3.5f;
    }

    private void Update()
    {
        if(gameStarted) return;
        
        timeTemp += Time.deltaTime;
        timeCountCircle.fillAmount = timeTemp / breathTime;
        var timeLeft = breathTime - timeTemp;
        breathText.text = $"Take a deep breath.\nGame starts in {timeLeft:0} seconds.";
        if(timeLeft > 3.5f) return;
        timeCountText.text = timeLeft.ToString("0");
        if(!DOTween.IsTweening(timeCountText))
            timeCountText.DOFade(1, 0.5f);
        if(!DOTween.IsTweening(skipButton))
            skipButton.DOFade(0, 0.5f).OnComplete(() => skipButton.gameObject.SetActive(false));

        if (!(timeLeft < 0)) return;
        gamePanel.SetActive(true);
        if(!DOTween.IsTweening(_canvasGroup))
            _canvasGroup.DOFade(0, fadeTime).OnComplete(() => gameObject.SetActive(false));
        gameStarted = true;
    }
}
