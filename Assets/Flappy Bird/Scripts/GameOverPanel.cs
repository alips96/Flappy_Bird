using Photon.Pun;
using UnityEngine;

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
