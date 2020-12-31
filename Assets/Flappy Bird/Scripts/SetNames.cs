using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SetNames : MonoBehaviour
{
    [SerializeField] private Text TextNames;
    private string name1;
    private string name2;

    private void OnEnable()
    {
        SendMyNameToOpponent();
        PhotonNetwork.NetworkingClient.EventReceived += SetPlayerNames;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += SetPlayerNames;
    }

    private void SendMyNameToOpponent()
    {
        string myName = PlayerPrefs.GetString("myName");

        //inform the other player
        PhotonNetwork.RaiseEvent(5, myName, new RaiseEventOptions { Receivers = ReceiverGroup.Others }, new SendOptions { Reliability = true });
    }

    private void SetPlayerNames(EventData obj)
    {
        if (obj.Code != 5)
            return;

        name1 = ToStandard(PlayerPrefs.GetString("myName"));
        name2 = ToStandard(obj.CustomData.ToString());

        if(TextNames != null)
            TextNames.text = name1 + " Vs " + name2;
    }

    private string ToStandard(string v)
    {
        StringBuilder newString = new StringBuilder(v);

        return newString.Replace(newString[0], char.ToUpper(newString[0]), 0, 1).ToString();
    }
}
