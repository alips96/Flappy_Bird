using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ConnectPhoton : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject inputFieldObject;
    [SerializeField] private GameObject statusText;

    private bool isConnecting;
    private const string gameVersion = "0.1";
    private const int maxPlayersPerRoom = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Screen.SetResolution(500, 500, false);
    }

    public void FindOpponent() //called by start button
    {
        InputField inputField = inputFieldObject.GetComponent<InputField>();

        if (inputField.text != null)
        {
            isConnecting = true;
            inputFieldObject.SetActive(false);
            statusText.SetActive(true);
            statusText.GetComponent<Text>().text = "Searching...";

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to photon");

        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public void DisconnectFromPhoton() //Called by cancel button.
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        isConnecting = false;
        statusText.SetActive(false);
        inputFieldObject.SetActive(true);

        Debug.Log($"Disconnected due to:  {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No clients waiting for an opponent, creating a new room");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client successfully joined a room.");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount != maxPlayersPerRoom)
        {
            statusText.GetComponent<Text>().text = "Waiting For Opponent..";
            Debug.Log("Client is waitng for an opponent");
        }
        else
        {
            statusText.GetComponent<Text>().text = "Opponent Found!";
            Debug.Log("Matching is ready to begin");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            Debug.Log("Opponent Found");
            statusText.GetComponent<Text>().text = "Match is ready to begin";

            PhotonNetwork.LoadLevel("GamePlay");
        }
    }
}
