using System.Collections;
using System.Linq;
using Managers;
using UISystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamePlay: MonoBehaviour, IEmsController
{
    public ScoreBoard scoreBoard;

    private void Start()
    {
        PauseMenu.Instance.EmsController = this;
    }

    public virtual void TurnOffAllEms()
    {
        print("Turn off All EMS!");
    }

    protected IEnumerator ActuateConnector(EMSConnectCell.Hand hand ,BluetoothConnector connector, int channel,float gapTime)
    {
        AudioManager.Instance.SFX.Play("computer_actuate");
        scoreBoard.PlayAnimation(hand);
        GameManager.SendChannelMessage(connector, channel, true);
        yield return new WaitForSeconds(gapTime);
        GameManager.SendChannelMessage(connector, channel, false);
    }
    
    protected static void SetUpPlayer<T>(out T leftHandInfo, out T rightHandInfo, out BluetoothConnector leftHandConnector, out BluetoothConnector rightHandConnector) where T : class
    {
        var mainPanels = FindObjectsOfType<GamePanel>().ToList();
        leftHandInfo = mainPanels.Find(p => p.hand == EMSConnectCell.Hand.LeftHand) as T;
        rightHandInfo = mainPanels.Find(p => p.hand == EMSConnectCell.Hand.RightHand) as T;
        var cells = FindObjectsOfType<EMSConnectCell>().ToList();
        leftHandConnector = cells.Find(c => c.hand == EMSConnectCell.Hand.LeftHand).connector;
        rightHandConnector = cells.Find(c => c.hand == EMSConnectCell.Hand.RightHand).connector;
    }
}