using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SevenPanel : GamePanel
{
    public int? TotalNumber { get; private set; }
    public int[] stepNumbers = new int[3];

    [SerializeField] private Sprite[] emojis = new Sprite[4];
    [SerializeField] private NumberBox totalNumberBox;
    [SerializeField] private NumberBox[] stepNumbersBoxes = new NumberBox[3];
    [SerializeField] private Image emojiImage;

    protected override void OnValidate()
    {
        base.OnValidate();
        UpdateNumbers();
        UpdateEmoji();
    }

    protected override void UpdateColors()
    {
        base.UpdateColors();
        totalNumberBox.color = color;
        foreach (var number in stepNumbersBoxes)
        {
            number.color = color;
        }
    }

    public void AddNumber(int number, int round)
    {
        if (round >= 3)
        {
            for (var i = 0; i < stepNumbers.Length; i++)
            {
                if (i != stepNumbers.Length - 1)
                    stepNumbers[i] = stepNumbers[i + 1];
                else
                    stepNumbers[i] = number;
            }
        }
        else
        {
            stepNumbers[round] = number;
        }

        TotalNumber ??= 0;
        TotalNumber += number;
        totalNumberBox.SetNumber(TotalNumber);
    }

    public void UpdateNumbers()
    {
        for (var i = 0; i < stepNumbersBoxes.Length; i++)
        {
            stepNumbersBoxes[i].SetNumber(stepNumbers[i] >= 0 ? stepNumbers[i] : null);
        }
    }

    public void UpdateEmoji()
    {
        emojiImage.sprite = TotalNumber switch
        {
            null => emojis[4],
            < 4 => emojis[0],
            < 7 => emojis[1],
            7 => emojis[2],
            _ => emojis[3]
        };
    }

    public override void ResetEverything()
    {
        TotalNumber = null;
        totalNumberBox.SetNumber(TotalNumber);
        stepNumbers = new[] {-1, -1, -1};
        UpdateNumbers();
        UpdateEmoji();
    }
}

