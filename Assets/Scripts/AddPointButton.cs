using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPointButton : MonoBehaviour
{
    public AudioClip Sound0, Sound1, Sound2;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound0()
    {
        audioSource.clip = Sound0;
        audioSource.Play();
    }

    public void PlaySound1()
    {
        audioSource.clip = Sound1;
        audioSource.Play();
    }

    public void PlaySound2()
    {
        audioSource.clip = Sound2;
        audioSource.Play();
    }

    public void OnDraw()
    {
        ScoreManager.Instance.ScoreChanged = true;
    }
}
