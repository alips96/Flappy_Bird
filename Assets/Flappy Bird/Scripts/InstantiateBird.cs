using Photon.Pun;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class InstantiateBird : MonoBehaviourPun
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private RuntimeAnimatorController opponentAnimator;

    private const float X_SPAWN_POS = -1.3f;
    private int ySpawnPos = 1;

    void Start()
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

    private void SetOpponentAnimation(GameObject playerInstance)
    {
        playerInstance.GetComponent<Animator>().runtimeAnimatorController = opponentAnimator;
    }

    private void SpawnPlayer(ulong clientId)
    {
        GameObject playerInstance = Instantiate(playerPrefab, new Vector2(X_SPAWN_POS, ySpawnPos++), Quaternion.identity);

        playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
    }
}
