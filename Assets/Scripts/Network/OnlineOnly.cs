using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class OnlineOnly : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Current.Network == GameManager.NetworkMode.Offline)
            Destroy(gameObject);
    }
}
