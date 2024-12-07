using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisconnectionHandler : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += ClientStopped;
    }

    private void ClientStopped(ulong disconnectedClientId)
    {
        if (disconnectedClientId == NetworkManager.Singleton.LocalClientId)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
