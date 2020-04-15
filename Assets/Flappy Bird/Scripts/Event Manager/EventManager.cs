using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;

public class EventManager : MonoBehaviour
{
    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventIncrementScore;

    public void CallEventGameOver()
    {
        //PhotonNetwork.RaiseEvent(1, spawnPos, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = false });
        PhotonNetwork.RaiseEvent(2, false, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
    }

    internal void CallEventIncrementScore()
    {
        EventIncrementScore.Invoke();
    }
}
