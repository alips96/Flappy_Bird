using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class EventManager : MonoBehaviour
{
    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventIncrementScore;

    public void CallEventGameOver(int actorNumber)
    {
        PhotonNetwork.RaiseEvent(2, actorNumber, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
    }

    internal void CallEventRestartLevel()
    {
        int levelNumber = 1;
        PhotonNetwork.RaiseEvent(3, levelNumber, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
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
