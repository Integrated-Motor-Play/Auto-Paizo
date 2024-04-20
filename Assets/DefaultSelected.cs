using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultSelected : MonoBehaviour
{
    [SerializeField] private bool actuateOnEnable; 
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        if(!actuateOnEnable)return;
        GetComponent<Button>().onClick.Invoke();
    }
}
