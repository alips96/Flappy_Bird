using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ApplyGameOver : MonoBehaviour
{
    [SerializeField] private GameObject loadLevelCanvas;
    private bool isWon = false;
    private Text victoryDialog;

    private MoveLeft moveLeft;
    private ColumnSpawner columnSpawner;

    private void OnEnable()
    {
        SetInitialReferences();
        PhotonNetwork.NetworkingClient.EventReceived += DetectPlayerToGameOver;
    }

    private void SetInitialReferences()
    {
        moveLeft = GameObject.Find("Ground").GetComponent<MoveLeft>();
        columnSpawner = GetComponent<ColumnSpawner>();
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= DetectPlayerToGameOver;
    }

    private void DetectPlayerToGameOver(EventData obj)
    {
        if(obj.Code == 2)
        {
            //Time.timeScale = 0;
            //loadLevelCanvas.SetActive(true);
            //loadLevelCanvas.GetComponentInChildren<Text>().text = Convert.ToString(obj.CustomData) + "and my local number is: " + PhotonNetwork.LocalPlayer.ActorNumber;

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
        //Time.timeScale = 0;
        columnSpawner.enabled = false;
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
}
