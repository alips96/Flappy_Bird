using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Text statusText;

    private const string EMPTY_NAME_MESSAGE = "Your name is empty! Try again.";
    private const int MAX_PLAYERS_COUNT = 2;

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log($"Client with ID {clientId} has connected to the server.");

        if (NetworkManager.Singleton.IsServer)
        {
            if (NetworkManager.Singleton.ConnectedClients.Count == MAX_PLAYERS_COUNT)
            {
                NetworkManager.Singleton.SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
            }
        }
    }

    public void OnStartClient(bool connectAsHost) //Called by UI buttons.
    {
        if (nameInputField.text.Length > 0)
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
        else
        {
            statusText.text = EMPTY_NAME_MESSAGE;
        }
    }
}
