using System.Linq;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class RestartLevel : NetworkBehaviour
{
    public void ReloadCurrentLevel() //Called by Restart button UI
    {
        ReloadLevelServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void ReloadLevelServerRpc()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }

    public void GotoMainMenu() //Called by Main Menue button UI
    {
        LoadMainMenuServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void LoadMainMenuServerRpc()
    {
        NetworkManager.Singleton.SceneManager.OnSceneEvent += HandleSceneEvent;
        NetworkManager.Singleton.SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void HandleSceneEvent(SceneEvent sceneEvent)
    {
        if (sceneEvent.SceneEventType == SceneEventType.LoadComplete &&
            sceneEvent.SceneName == "MainMenu" &&
            NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.SceneManager.OnSceneEvent -= HandleSceneEvent;
            DisconnectClients();
        }
    }

    private static void DisconnectClients()
    {
        foreach (var clientId in NetworkManager.Singleton.ConnectedClientsIds.Skip(1))
        {
            NetworkManager.Singleton.DisconnectClient(clientId);
        }

        NetworkManager.Singleton.Shutdown(); //shutdown server
    }
}
