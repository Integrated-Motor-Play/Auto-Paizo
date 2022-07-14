using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzerTimeShower : MonoBehaviour
{
    public GameObject warningPanel;
    public float buzzerTime = 1800;
    private bool _activated;

    private void Update()
    {
        if (!(Time.time > buzzerTime) || _activated) return;
        warningPanel.SetActive(true);
        _activated = true;
    }
}
