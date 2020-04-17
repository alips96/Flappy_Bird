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

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += DetectPlayerToGameOver;
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
        FreezeGame();
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

    private void FreezeGame()
    {
        MoveLeft groundMovement = GameObject.Find("Ground").GetComponent<MoveLeft>();
        ColumnSpawner columnSpawner = GetComponent<ColumnSpawner>();

        groundMovement.enabled = false;
        columnSpawner.enabled = false;

        GameObject[] columns = GameObject.FindGameObjectsWithTag("Column");
        GameObject bird = GameObject.FindGameObjectWithTag("Player");

        bird.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        //bird.GetComponent<Animator>().enabled = false;

        foreach (GameObject item in columns)
        {
            item.GetComponent<MoveLeft>().enabled = false;
        }

        
    }
}
