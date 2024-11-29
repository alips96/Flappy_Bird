using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class InstantiateBird : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private RuntimeAnimatorController opponentAnimator;

    public float xSpawnPos = -1.3f;
    private int ySpawnPos = 1;

    private void Start()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            SpawnPlayer(NetworkManager.Singleton.LocalClientId); //spawn host

            var connectedClientIds = NetworkManager.Singleton.ConnectedClientsIds;

            foreach (var clientId in connectedClientIds.Skip(1))
            {
                SpawnPlayer(clientId);
            }
        }
    }

    private void SpawnPlayer(ulong clientId)
    {
        GameObject playerInstance = Instantiate(playerPrefab, new Vector2(xSpawnPos, ySpawnPos), Quaternion.identity);

        playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
    }
}
