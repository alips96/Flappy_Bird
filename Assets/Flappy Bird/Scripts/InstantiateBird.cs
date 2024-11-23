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

        player = GameObject.Instantiate(playerPrefab, new Vector3(xSpawnPos, 1, 0), Quaternion.identity);

        player.GetComponent<Animator>().runtimeAnimatorController = opponentAnimator;

    }
}
