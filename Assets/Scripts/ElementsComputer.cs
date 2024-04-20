
using System;
using System.Collections;
using System.Linq;
using Managers;
using UISystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class ElementsComputer:GamePlay
{
    private Player _playerLeftHand, _playerRightHand;
    
    public enum Element
    {
        Metal,
        Water,
        Wood,
        Fire,
        Earth
    }

    private struct Player
    {
        public ElementPanel Info;
        public BluetoothConnector Connector;
    }
    
    public class ElementInfo
    {
        public readonly Element Element;
        public readonly Element[] BeatElements;

        public ElementInfo(Element element)
        {
            Element = element;
            BeatElements = Element switch
            {
                Element.Metal => new Element[2] {Element.Earth, Element.Wood},
                Element.Water => new Element[2] {Element.Metal, Element.Fire},
                Element.Wood => new Element[2] {Element.Water, Element.Earth},
                Element.Fire => new Element[2] {Element.Wood, Element.Metal},
                Element.Earth => new Element[2] {Element.Fire, Element.Water},
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public int Channel
        {
            get
            {
                return Element switch
                {
                    Element.Metal => 2,
                    Element.Water => 0,
                    Element.Wood => 3,
                    Element.Fire => 1,
                    Element.Earth => 4,
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
            yield return  scoreBoard.SetRoundText(i + 1);
            AudioManager.Instance.SFX.Play("computer_round");
            yield return new WaitForSeconds(3);
            StartCoroutine(ActuatePlayer(_playerLeftHand, i));
            yield return ActuatePlayer(_playerRightHand, i);
            if (_playerLeftHand.Info.Element.BeatElements.Contains(_playerRightHand.Info.Element.Element))
            {
                scoreBoard.LeftGetsPoint($"<b>{_playerLeftHand.Info.Element.Element}</b> beats <b>{_playerRightHand.Info.Element.Element}</b>!");
            }
            else if (_playerRightHand.Info.Element.BeatElements.Contains(_playerLeftHand.Info.Element.Element))
            {
                scoreBoard.RightGetsPoint($"<b>{_playerLeftHand.Info.Element.Element}</b> loses to <b>{_playerRightHand.Info.Element.Element}</b>!");
            }
            else 
            {
                scoreBoard.NoOneGetsPoint($"Oops! \n<b>{_playerRightHand.Info.Element.Element}</b> and <b>{_playerLeftHand.Info.Element.Element}</b>,");
            }

            _playerLeftHand.Info.ResetEverything();
            _playerRightHand.Info.ResetEverything();
            
            if(scoreBoard.CheckIfOneWins()) break;
        }
        
        yield return new WaitForSeconds(3);
        
        scoreBoard.RevealFinalScoreBoard();
    }

    private IEnumerator ActuatePlayer(Player player, int round)
    {
        var element = GetRandomNumber();
        print("Round "+round+", Channel " + element.Channel +": " + element.Element);
        yield return ActuateConnector(player.Info.hand, player.Connector, element.Channel, 1.5f);
        player.Info.Element = element;
        player.Info.UpdateElementImage();
        yield return new WaitForSeconds(3);
    }
    
    private static ElementInfo GetRandomNumber()
    {
        var element = new ElementInfo((Element)Random.Range(0, Enum.GetValues(typeof(Element)).Length));
        return element;
    }
    
    public override void TurnOffAllEms()
    {
        _playerLeftHand.Connector.SendBluetoothData("r");
        _playerRightHand.Connector.SendBluetoothData("r");
    }
}
