using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class RestartLevel : MonoBehaviour
{
    [SerializeField] private GameObject textInfo;

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

        if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else
        {
            textInfo.SetActive(true);
        }
    }
}
