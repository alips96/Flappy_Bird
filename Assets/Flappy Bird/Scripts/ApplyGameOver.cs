using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ApplyGameOver : NetworkBehaviour
{
    [SerializeField] private GameObject loadLevelCanvas;
    private Text victoryDialog;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text playerNames;
    [SerializeField] private Text otherScoreText;
    [SerializeField] private Text otherplayerNames;

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
        FreezeGame();
        GameOverClientRpc(loserClientId);
        //DisconnectClients();
    }

    private static void DisconnectClients()
    {
        foreach (var clientId in NetworkManager.Singleton.ConnectedClientsIds.Skip(1))
        {
            NetworkManager.Singleton.DisconnectClient(clientId);
        }

        // Shutdown the server
        NetworkManager.Singleton.Shutdown();
    }

    [ClientRpc]
    private void GameOverClientRpc(ulong loserClientId)
    {
        //SetNamesAndScore(); //TBD
        loadLevelCanvas.SetActive(true);
        victoryDialog = loadLevelCanvas.GetComponentInChildren<Text>();

        if (NetworkManager.Singleton.LocalClientId == loserClientId)
        {
            victoryDialog.color = Color.red;
            loadLevelCanvas.GetComponentInChildren<Text>().text = "You Lost :(";
        }
        else
        {
            victoryDialog.color = Color.green;
            loadLevelCanvas.GetComponentInChildren<Text>().text = "You Won!";
        }
    }

    private void SetNamesAndScore()
    {
        ScoreText.text = otherScoreText.text;
        playerNames.text = otherplayerNames.text;
    }

    public void FreezeGame()
    {
        MoveLeft groundMovement = GameObject.Find("Ground").GetComponent<MoveLeft>();
        ColumnSpawner columnSpawner = GetComponent<ColumnSpawner>();

        groundMovement.enabled = false;
        columnSpawner.enabled = false;

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
