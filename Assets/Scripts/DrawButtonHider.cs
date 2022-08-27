using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class DrawButtonHider : MonoBehaviour
{
    private void OnEnable()
    {
        if (GameManager.Current.Game != GameManager.Game.Elements)
        {
            gameObject.SetActive(false);
        }
    }
}
