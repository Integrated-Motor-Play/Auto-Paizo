using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class GameFilter : MonoBehaviour
{
    public GameManager.Game[] thisGames;

    private void Start()
    {
        var setTure = false;
        foreach (var game in thisGames)
        {
            if (game == GameManager.Current.Game)
                setTure = true;
        }
        gameObject.SetActive(setTure);
    }
}
