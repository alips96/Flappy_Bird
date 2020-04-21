using Photon.Pun;
using UnityEngine;

public class InstantiateBird : MonoBehaviourPun
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private RuntimeAnimatorController opponentAnimator;
    
    private const float xSpawnPos = -1.3f;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateBirdPrefab();
    }

    private void InstantiateBirdPrefab()
    {
        //Set Prefabs
        if (PhotonNetwork.IsMasterClient)
        {
            player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(xSpawnPos, 1, 0), Quaternion.identity);
        }
        else
        {
            player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(xSpawnPos, 0, 0), Quaternion.identity);
        }

        //Set Animation
        if (player.GetPhotonView().IsMine)
        {
            player.GetComponent<Animator>().runtimeAnimatorController = opponentAnimator;
        }
    }
}
