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
        //Screen.SetResolution(500, 500, false);
    }

    public void FindOpponent() //called by start button
    {
        InputField inputField = inputFieldObject.GetComponent<InputField>();

        if (inputField.text.Length > 0)
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

    private void SaveCurrentClientName(string text)
    {
        PlayerPrefs.SetString("myName", text);
    }

    public override void OnConnectedToMaster()
    {
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
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        SaveCurrentClientName(inputFieldObject.GetComponent<InputField>().text);

        if (playerCount != maxPlayersPerRoom)
        {
            statusText.GetComponent<Text>().text = "Waiting For Opponent..";
        }
        else
        {
            statusText.GetComponent<Text>().text = "Opponent Found!";
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            statusText.GetComponent<Text>().text = "Match is ready to begin";
            PhotonNetwork.LoadLevel("GamePlay");
        }
    }
}
