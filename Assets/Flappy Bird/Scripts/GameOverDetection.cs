using Unity.Netcode;
using UnityEngine;

public class GameOverDetection : NetworkBehaviour
{
    private EventManager eventMaster;

    private void Start()
    {
        eventMaster = GameObject.Find("GameManager").GetComponent<EventManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsOwner)
        {
            NotifyGameOverServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = true)]
    private void NotifyGameOverServerRpc()
    {
        Debug.Log("Game over occured! " + OwnerClientId);
        eventMaster.CallEventGameOver(OwnerClientId);
    }
}
