using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCircle : MonoBehaviour
{
    public float delay;

    private void OnEnable()
    {
        Invoke(nameof(StartAnimation), delay);
    }

    private void StartAnimation()
    {
        GetComponent<Animator>().enabled = true;
    }

    private void OnDisable()
    {
        GetComponent<Animator>().enabled = false;
    }
}
