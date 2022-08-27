using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UISystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class SevenComputer : GamePlay
{
    private Player _playerLeftHand, _playerRightHand;

    public enum Number
    {
        N0,
        N1,
        N2,
        N4,
        N5
    }

    private struct Player
    {
        public SevenPanel Info;
        public BluetoothConnector Connector;
    }

    public struct NumberInfo
    {
        public Number Number;
        public int Value
        {
            get
            {
                return Number switch
                {
                    Number.N0 => 0,
                    Number.N1 => 1,
                    Number.N2 => 2,
                    Number.N4 => 4,
                    Number.N5 => 5,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        public int Channel
        {
            get
            {
                return Number switch
                {
                    Number.N0 => 4,
                    Number.N1 => 3,
                    Number.N2 => 1,
                    Number.N4 => 2,
                    Number.N5 => 0,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }

    private void Awake()
    {
        SetUpPlayer(out _playerLeftHand.Info, out _playerRightHand.Info
            , out _playerLeftHand.Connector, out _playerRightHand.Connector);
        
        StartCoroutine(PlayGame());
        scoreBoard.SetColor(_playerLeftHand.Info.hand, _playerLeftHand.Info.color);
        scoreBoard.SetColor(_playerRightHand.Info.hand, _playerRightHand.Info.color);
    }

    private IEnumerator PlayGame()
    {
        for (var i = 0; i < GameManager.Current.RoundValue; i++)
        {
            yield return scoreBoard.SetRoundText(i + 1);
            AudioManager.Instance.SFX.Play("computer_round");
            yield return PlayRound();

            _playerLeftHand.Info.ResetEverything();
            _playerRightHand.Info.ResetEverything();
            
            if(scoreBoard.CheckIfOneWins()) break;
        }
        
        yield return new WaitForSeconds(3);
        
        scoreBoard.RevealFinalScoreBoard();
    }

    private IEnumerator PlayRound()
    {
        yield return new WaitForSeconds(3);
        var leftFirst = Random.Range(0f, 1f) > 0.5;
        var p1 = leftFirst? _playerLeftHand : _playerRightHand;
        var p2 = leftFirst? _playerRightHand: _playerLeftHand;
        for (var i = 0; i < 100; i++)
        {
            yield return ActuatePlayer(p1, i);
            if(CheckPlayerWin(p1, leftFirst)) break;
            yield return ActuatePlayer(p2, i);
            if(CheckPlayerWin(p2, !leftFirst)) break;
        }
    }

    private bool CheckPlayerWin(Player player, bool leftFirst)
    {
        switch (player.Info.TotalNumber)
        {
            case 7:
                if(leftFirst) scoreBoard.LeftGetsPoint("Left Hand Got <color=yellow><size=200>7</size></color>!");
                else scoreBoard.RightGetsPoint("Right Hand Got <color=yellow><size=200>7</size></color>!");
                return true;
            case > 7:
                if(leftFirst) scoreBoard.RightGetsPoint("Left Hand <color=red><size=150>Busts</size></color>!");
                else scoreBoard.LeftGetsPoint("Right Hand <color=red><size=150>Busts</size></color>!"); 
                return true;
        }

        return false;
    }

    private IEnumerator ActuatePlayer(Player player, int round)
    {
        var number = GetRandomNumber();
        print("Round "+round+", Channel " + number.Channel +": " + number.Value);
        yield return ActuateConnector(player.Info.hand, player.Connector, number.Channel, 1.5f);
        player.Info.AddNumber(number.Value, round);
        player.Info.UpdateNumbers();
        player.Info.UpdateEmoji();
        yield return new WaitForSeconds(3);
    }

    private static NumberInfo GetRandomNumber()
    {
        var number = new NumberInfo
        {
            Number = (Number)Random.Range(0, Enum.GetValues(typeof(Number)).Length)
        };
        return number;
    }

    public override void TurnOffAllEms()
    {
        _playerLeftHand.Connector.SendBluetoothData("r");
        _playerRightHand.Connector.SendBluetoothData("r");
    }
}
