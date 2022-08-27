using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using TMPro;
using UISystem;
using UnityEngine;

public class MatchPanel : MonoBehaviour, IEmsController
{
    public TablePlayerHandHolder player1Holder, player2Holder;
    public Gesture[] gestures;
    private GestureElement[] _gestureElements;
    private List<Gesture> _listGesture = new List<Gesture>();
    private Player _player1, _player2;
    private bool _singlePlayer;
    public GameObject finalScoreBoard;

    public TextMeshProUGUI roundText ,finalScore , highScore, lowScore;

    private string highKey, lowKey;

    private readonly struct Player
    {
        public readonly TablePlayerHandHolder Info;
        public Player(TablePlayerHandHolder holder)
        {
            Info = holder;
            var cells = FindObjectsOfType<EMSConnectCell>().ToList();
            Info.LeftHand.Connector = cells.Find(c => c.hand == EMSConnectCell.Hand.LeftHand && c.player == holder.player).connector;;
            Info.RightHand.Connector = cells.Find(c => c.hand == EMSConnectCell.Hand.RightHand && c.player == holder.player).connector;;
        }

        public void ResetGestures()
        {
            Info.LeftHand.gameObject.SetActive(false);
            Info.RightHand.gameObject.SetActive(false);
        }
    }
    
    private void Awake()
    {
        _player1 = new Player(player1Holder);
        if (player2Holder == null)
            _singlePlayer = true;
        if(!_singlePlayer)
            _player2 = new Player(player2Holder);
        
        _gestureElements = FindObjectsOfType<GestureElement>();
        _listGesture = gestures.ToList();
        StartCoroutine(PlayGame());

        highKey = _singlePlayer ? "Match1HighRound" : "MatchHighRound";
        lowKey = _singlePlayer ? "Match1LowRound" : "MatchLowRound";

        var high = PlayerPrefs.HasKey(highKey)
            ? PlayerPrefs.GetInt(highKey).ToString()
            : "";
        highScore.text = "Highest Round: " + high;
        var low = PlayerPrefs.HasKey(lowKey)
            ? PlayerPrefs.GetInt(lowKey).ToString()
            : "";
        lowScore.text = "Lowest Round: " + low;
    }
    
    private void Start()
    {
        PauseMenu.Instance.EmsController = this;
    }

    private IEnumerator PlayGame()
    {
        var round = 0;
        for (var i = 0; i < GameManager.Current.RoundValue; i++)
        {
            round = i + 1;
            roundText.text = "3";
            yield return new WaitForSeconds(1);
            roundText.text = "2";
            yield return new WaitForSeconds(1);
            roundText.text = "1";
            yield return new WaitForSeconds(1);
            roundText.text = "Round " + round;
            AudioManager.Instance.SFX.Play("computer_round");
            yield return new WaitForSeconds(3);
            if(!_singlePlayer){
                StartCoroutine(ActuatePlayer(_player2.Info.LeftHand, i));
                StartCoroutine(ActuatePlayer(_player2.Info.RightHand, i));
            }
            StartCoroutine(ActuatePlayer(_player1.Info.LeftHand, i));
            yield return ActuatePlayer(_player1.Info.RightHand, i);

            var tableGestures = _singlePlayer? new[]
            {
                _player1.Info.LeftHand, _player1.Info.RightHand
            } : new[]
            {
                _player1.Info.LeftHand, _player1.Info.RightHand, 
                _player2.Info.LeftHand, _player2.Info.RightHand
            } ;

            var exceedNumber = _singlePlayer ? 2 : 3;

            var matchedGesture = tableGestures.GroupBy(x => x.Gesture)
                .ToList();
            if (matchedGesture.Max(c => c.Count()) >= exceedNumber)
            {
                var gestureList = matchedGesture.First(c => c.Count() >= exceedNumber).ToList();
                var element = _gestureElements.ToList().Find(c => c.gesture == gestureList[0].Gesture);
                element.Eliminate();
                foreach (var ges in gestureList)
                {
                    ges.PlayMatchedAnimation();
                }
                print("Matched! " + gestureList[0].Gesture.gestureName);
                _listGesture.Remove(gestureList[0].Gesture);
                AudioManager.Instance.SFX.Play("computer_match", 1, 1);
            }
            
            yield return new WaitForSeconds(3);
            
            _player1.ResetGestures();
            if(!_singlePlayer)
                _player2.ResetGestures();
            
            if(GestureElement.GetEliminatedCount() == 5) break;
        }
        
        yield return new WaitForSeconds(3);

        finalScore.text = $"Your Took {round} Rounds to Match!";
        var higher = round > PlayerPrefs.GetInt(highKey, 0);
        var lower = round < PlayerPrefs.GetInt(lowKey, int.MaxValue);
        if (higher)
        {
            finalScore.text = "You Got a new High Round: " + round;
            PlayerPrefs.SetInt(highKey, round);
        }
        if (lower)
        {
            finalScore.text = "You Got a new Low Round: " + round;
            PlayerPrefs.SetInt(lowKey, round);
        }
        if(higher && lower)
            finalScore.text = "You Got a new High & Low Round: " + round;

        AudioManager.Instance.SFX.Play("computer_match_end");
        finalScoreBoard.SetActive(true);
        
        //scoreBoard.RevealFinalScoreBoard();
    }
    
    private IEnumerator ActuatePlayer(TableGesture hand, int round)
    {
        var gesture = _listGesture[Random.Range(0,_listGesture.Count)];
        print("Round "+round+", Channel " + gesture.channel +": " + gesture.gestureName);
        yield return ActuateConnector(hand.hand, hand.Connector, gesture.channel, 1.5f);
        hand.Gesture = gesture;
        hand.gameObject.SetActive(true);
    }
    
    private static IEnumerator ActuateConnector(EMSConnectCell.Hand hand ,BluetoothConnector connector, int channel,float gapTime)
    {
        AudioManager.Instance.SFX.Play("computer_actuate");
        GameManager.SendChannelMessage(connector, channel, true);
        yield return new WaitForSeconds(gapTime);
        GameManager.SendChannelMessage(connector, channel, false);
    }

    public void TurnOffAllEms()
    {
        _player1.Info.LeftHand.Connector.SendBluetoothData("r");
        _player1.Info.RightHand.Connector.SendBluetoothData("r");
        if (_singlePlayer) return;
        _player2.Info.LeftHand.Connector.SendBluetoothData("r");
        _player2.Info.RightHand.Connector.SendBluetoothData("r");
    }
}
