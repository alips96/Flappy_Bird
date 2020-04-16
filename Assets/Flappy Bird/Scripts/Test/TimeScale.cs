using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class TimeScale : MonoBehaviour
{
    private void OnEnable()
    {
        //PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NetworkingClient.EventReceived += TimeScaleToOne;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= TimeScaleToOne;
    }

    private void TimeScaleToOne(EventData obj)
    {
        if (obj.Code != 5)
            return;

        Time.timeScale = 1;
    }
}
