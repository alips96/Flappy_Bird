using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class EventManager : MonoBehaviour
{
    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventIncrementScore;
    public event GeneralEventHandler EventGameOver;

    public void CallEventGameOver()
    {
        EventGameOver?.Invoke();
    }

    internal void CallEventGotoMainMenu()
    {
        int levelNumber = 0;
        PhotonNetwork.RaiseEvent(4, levelNumber, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
    }

    internal void CallEventIncrementScore()
    {
        EventIncrementScore.Invoke();
    }
}
