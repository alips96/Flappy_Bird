using Unity.Netcode;
using UnityEngine;

public class HelloWorldManager : MonoBehaviour
{
    private NetworkManager m_NetworkManager;

    void Start()
    {
        m_NetworkManager = NetworkManager.Singleton;
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!m_NetworkManager.IsClient && !m_NetworkManager.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();

            SubmitNewPosition();
        }

        GUILayout.EndArea();
    }

    private void StartButtons()
    {
        if (GUILayout.Button("Host")) m_NetworkManager.StartHost();
        if (GUILayout.Button("Client")) m_NetworkManager.StartClient();
        if (GUILayout.Button("Server")) m_NetworkManager.StartServer();
    }

    private void StatusLabels()
    {
        var mode = m_NetworkManager.IsHost ?
            "Host" : m_NetworkManager.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            m_NetworkManager.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }

    private void SubmitNewPosition()
    {
        if (GUILayout.Button(m_NetworkManager.IsServer ? "Move" : "Request Position Change"))
        {
            //if (m_NetworkManager.IsHost)
            //{
            //    Debug.Log(m_NetworkManager.LocalClientId);
            //    GUILayout.Label("I am the host " + m_NetworkManager.LocalClientId);
            //}
            if (m_NetworkManager.IsServer && !m_NetworkManager.IsClient)
            {
                foreach (ulong uid in m_NetworkManager.ConnectedClientsIds)
                    GUILayout.Label(uid + "is here");
            }
            else
            {
                NetworkObject playerObject = m_NetworkManager.SpawnManager.GetLocalPlayerObject();
                GUILayout.Label("playerobject name is" + playerObject.transform.name);
                Debug.Log("playerobject name is" + playerObject.transform.name);
            }
        }
    }
}
