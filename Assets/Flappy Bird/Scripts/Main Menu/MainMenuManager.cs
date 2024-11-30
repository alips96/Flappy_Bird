using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Text statusText;

    [SerializeField] private int maxPlayersCount = 2;

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log($"Client with ID {clientId} has connected to the server.");

        if (NetworkManager.Singleton.IsServer)
        {
            if (NetworkManager.Singleton.ConnectedClients.Count == maxPlayersCount)
            {
                NetworkManager.Singleton.SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
            }
        }
    }

    public void OnStartClient(bool connectAsHost) //Called by UI buttons.
    {
        try
        {
            if (connectAsHost)
            {
                NetworkManager.Singleton.StartHost();
            }
            else
            {
                NetworkManager.Singleton.StartClient();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
            statusText.text = "Failed to connect client.";
        }
    }

    public void QuitGame() //Called by Quit button.
    {
        Application.Quit();
    }
}
