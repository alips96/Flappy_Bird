using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class EventManager : MonoBehaviour
{
    public void CallEventGameOver()
    {
        //PhotonNetwork.RaiseEvent(1, spawnPos, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = false });
        PhotonNetwork.RaiseEvent(2, false, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
    }
}
