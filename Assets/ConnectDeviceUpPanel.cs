using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectDeviceUpPanel : MonoBehaviour
{
    public static ConnectDeviceUpPanel Instance;
    public DeviceDetailPanel deviceDetailPanel;

    private void Awake()
    {
        Instance = this;
    }
}
