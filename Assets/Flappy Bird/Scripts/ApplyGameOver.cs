using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ApplyGameOver : MonoBehaviour
{
    [SerializeField] private GameObject loadLevelCanvas;
    private bool isWon = false;
    private Text victoryDialog;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text playerNames;
    [SerializeField] private Text otherScoreText;
    [SerializeField] private Text otherplayerNames;

    private void OnEnable() => PhotonNetwork.NetworkingClient.EventReceived += DetectPlayerToGameOver;

    private void OnDisable() => PhotonNetwork.NetworkingClient.EventReceived -= DetectPlayerToGameOver;

    private void DetectPlayerToGameOver(EventData obj)
    {
        if(obj.Code == 2)
        {
            int clientId = (int)obj.CustomData;

            if (clientId != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                isWon = true;
            }

            PerformGameOver();
        }
    }

    private void PerformGameOver()
    {
        FreezeGame();
        SetNamesAndScore();
        loadLevelCanvas.SetActive(true);
        victoryDialog = loadLevelCanvas.GetComponentInChildren<Text>();

        if (isWon)
        {
            victoryDialog.color = Color.green;
            loadLevelCanvas.GetComponentInChildren<Text>().text = "You Won!";
        }
        else
        {
            victoryDialog.color = Color.red;
            loadLevelCanvas.GetComponentInChildren<Text>().text = "You lost :(";
        }
    }

    private void SetNamesAndScore()
    {
        ScoreText.text = otherScoreText.text;
        playerNames.text = otherplayerNames.text;
    }

    private void FreezeGame()
    {
        MoveLeft groundMovement = GameObject.Find("Ground").GetComponent<MoveLeft>();
        ColumnSpawner columnSpawner = GetComponent<ColumnSpawner>();

        groundMovement.enabled = false;
        columnSpawner.enabled = false;

        GameObject[] columns = GameObject.FindGameObjectsWithTag("Column");
        GameObject bird = GameObject.FindGameObjectWithTag("Player");
        Destroy(bird);

        foreach (GameObject item in columns)
        {
            Destroy(item);
        }
    }
}
