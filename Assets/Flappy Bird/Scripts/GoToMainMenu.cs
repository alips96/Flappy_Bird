using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using System.Collections;

public class GoToMainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += LoadMainMenuScene;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= LoadMainMenuScene;
    }

    private void LoadMainMenuScene(EventData obj)
    {
        if (obj.Code != 4)
            return;

        DisconnectPlayer();
    }

    private void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();

        while (PhotonNetwork.IsConnected)
            yield return null;

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}