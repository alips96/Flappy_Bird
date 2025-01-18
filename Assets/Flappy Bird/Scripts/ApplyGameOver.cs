using Unity.Netcode;
using UnityEngine;

public class ApplyGameOver : NetworkBehaviour
{
    [SerializeField] private GameObject loadLevelCanvas;
    [SerializeField] private TMPro.TMP_Text victoryDialog;
    [SerializeField] private TMPro.TMP_Text ScoreText;
    [SerializeField] private MoveLeft groundMovement;

    private EventManager eventManager;

    private void OnEnable()
    {
        SetInitialReferences();

        eventManager.EventGameOver += PerformGameOver;
    }

    private void SetInitialReferences()
    {
        eventManager = GetComponent<EventManager>();
    }

    private void OnDisable()
    {
        eventManager.EventGameOver -= PerformGameOver;
    }

    private void PerformGameOver(ulong loserClientId) //Run by server.
    {
        FreezeGameClientRpc();
        DestroyNetworkObjects();
        GameOverClientRpc(loserClientId);
    }

    [ClientRpc]
    private void GameOverClientRpc(ulong loserClientId)
    {
        loadLevelCanvas.SetActive(true);

        if (NetworkManager.Singleton.LocalClientId == loserClientId)
        {
            victoryDialog.color = Color.red;
            victoryDialog.text = "You Lost :(";
        }
        else
        {
            victoryDialog.color = Color.green;
            victoryDialog.text = "You Won!";
        }
    }

    [ClientRpc]
    public void FreezeGameClientRpc()
    {
        ColumnSpawner columnSpawner = GetComponent<ColumnSpawner>();

        groundMovement.enabled = false;
        columnSpawner.enabled = false;
    }

    private void DestroyNetworkObjects()
    {
        GameObject[] columns = GameObject.FindGameObjectsWithTag("Column");
        GameObject[] birds = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject item in columns)
        {
            Destroy(item);
        }

        foreach (var bird in birds)
        {
            Destroy(bird);
        }
    }
}
