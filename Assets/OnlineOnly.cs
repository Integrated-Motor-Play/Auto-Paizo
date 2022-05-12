using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineOnly : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.networkMode == GameManager.NetworkMode.offline)
            Destroy(gameObject);
    }
}
