using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += ReloadCurrentLevel;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= ReloadCurrentLevel;
    }

    private void ReloadCurrentLevel(EventData obj)
    {
        if (obj.Code != 3)
            return;

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        PhotonNetwork.LoadLevel("GamePlay");
    }
}
