using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetworkLauncher : MonoBehaviourPunCallbacks
{

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    public TextMeshProUGUI ConnectMessage;
    public TextMeshProUGUI PlayersText;
    public GameObject ConnectPanelNextButton;
    public TMP_InputField CreateRoomName, JoinRoomName;
    public TextMeshProUGUI LobbyTitle;

    public GameObject LobbyPanelNextButton;

    string gameVersion = "1";

    void Awake()
    {
        //PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        Connect();
    }

    public void CreateRoom()
    {
        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(CreateRoomName.text, new RoomOptions { MaxPlayers = maxPlayersPerRoom, PublishUserId = true });
    }
    public void JoinRoom()
    {
        // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
        PhotonNetwork.JoinRoom(JoinRoomName.text);
    }

    public void Connect()
    {
        ConnectMessage.text = "Connecting...";
        // #Critical, we must first and foremost connect to Photon Online Server.
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }


    public override void OnConnectedToMaster()
    {
        SetPlayerName(GameManager.playerName);
        ConnectPanelNextButton.SetActive(true);
        ConnectMessage.text = "Sucessfully connected to the Server\n" + PhotonNetwork.ServerAddress;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        ConnectMessage.text = "Disconnected\n";
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
    }

    public override void OnJoinedRoom()
    {
        LobbyTitle.text = "Room: " + PhotonNetwork.CurrentRoom.Name;
        LobbyPanelNextButton.SetActive(true);
    }

    public void SetPlayerName(string value)
    {
        // #Important
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        print("Record Nick Name: " + GameManager.playerName);
        PhotonNetwork.NickName = value;
        print("My Nick Name: " + PhotonNetwork.NickName);
    }

    private void Update()
    {
        PlayersText.text = "Players List:\n";
        foreach (var pl in PhotonNetwork.PlayerList)
        {
            PlayersText.text += pl.NickName + "\n";
        }
    }
}