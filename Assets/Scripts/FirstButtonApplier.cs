using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstButtonApplier : MonoBehaviour
{
    public Transform objectParent;
    private GameObject lastSelect;

    private void OnEnable()
    {
        GameManager.Current.Round = SetSelectedObject().GetComponent<ModeButtonController>().currentMode;
    }

    private void Update()
    {
        SetSelectedObject();
    }

    private GameObject SetSelectedObject()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect != null
                ? lastSelect
                : objectParent.GetChild(0).gameObject);
        }
        else
        {
            var obj = EventSystem.current.currentSelectedGameObject;
            if (obj.GetComponent<ModeButtonController>() != null)
                lastSelect = obj;
            else
                EventSystem.current.SetSelectedGameObject(lastSelect);
        }

        return objectParent.GetChild(0).gameObject;
    }
    
    public void SwitchMode()
    {
        GameManager.Current.Round = EventSystem.current.currentSelectedGameObject.GetComponent<ModeButtonController>().currentMode;
    }
}
