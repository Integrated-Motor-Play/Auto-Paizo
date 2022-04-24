using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    private Player myPlayer;

    public Dictionary<string, string> playerActions;

    private void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.IsMine)
        {
            NetworkPlayer.LocalPlayerInstance = this.gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(playerActions);
            stream.SendNext(myPlayer);
        }
        else
        {
            // Network player, receive data
            this.playerActions = (Dictionary<string, string>)stream.ReceiveNext();
            this.myPlayer = (Player)stream.ReceiveNext();
        }
        if (playerActions != null)
        {
            print("Received Data: " + playerActions.Count);
            SendLocalBluetoothData();
        }
    }

    private void Update()
    {

    }

    public void SendLocalBluetoothData()
    {
        print("OKay");
        foreach (var plAc in playerActions)
        {
            print("Player: " + plAc.Key + ", " + plAc.Value);
            if (plAc.Key == PhotonNetwork.LocalPlayer.UserId)
            {
                GameManager.SendBluetoothData(plAc.Value);
            }
        }
        playerActions = null;
    }
}
