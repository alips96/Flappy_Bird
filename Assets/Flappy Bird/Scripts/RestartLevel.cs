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
        SceneManager.LoadScene(0);
    }
}
