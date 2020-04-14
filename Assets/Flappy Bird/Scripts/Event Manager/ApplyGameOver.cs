using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

public class ApplyGameOver : MonoBehaviour
{
    [SerializeField] private GameObject loadLevelCanvas;

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += PerformGameOver;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= PerformGameOver;
    }

    private void PerformGameOver(EventData obj)
    {
        if(obj.Code == 2)
        {
            Time.timeScale = 0;
            loadLevelCanvas.SetActive(true);
        }
    }
}
