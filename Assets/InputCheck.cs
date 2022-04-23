using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCheck : MonoBehaviour
{
    public GameObject beginButton, checkMark;

    public void CheckNameWords(string name)
    {
        beginButton.SetActive(name.Length > 0);
        checkMark.SetActive(name.Length > 0);
    }
}
