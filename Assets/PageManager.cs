using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public List<GameObject> pages;
    public GameObject nextButton, lastButton;

    private int _currentPage;

    private void OnEnable()
    {
        UpdateButtons();
    }

    public void GoPage(int offset)
    {
        pages[_currentPage].SetActive(false);
        var page = GetPage(offset);
        pages[page].GetComponent<AnimationLeadIn>().distance = -offset * 0.5f;
        pages[page].SetActive(true);
        _currentPage = page;
        UpdateButtons();
    }
    
    private void UpdateButtons()
    {
        lastButton.SetActive(_currentPage != 0);
        nextButton.SetActive(_currentPage != pages.Count - 1);
    }

    private int GetPage(int offset)
    {
        var page = _currentPage + offset;
        page %= pages.Count;
        return page;
    }

    private void OnDisable()
    {
        foreach (var page in pages)
        {
            page.GetComponent<AnimationLeadIn>().distance = 0;
        }
    }
}
