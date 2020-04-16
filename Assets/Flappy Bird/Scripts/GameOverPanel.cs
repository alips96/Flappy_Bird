using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    private EventManager eventManager;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        SetInitialReferences();
    }

    private void SetInitialReferences()
    {
        eventManager = GetComponent<EventManager>();
    }

    public void ToMainMenu() //Called by home button
    {
        eventManager.CallEventGotoMainMenu();
    }

    public void RestartLevel() //Called by restart Level
    {
        eventManager.CallEventRestartLevel();
    }
}
