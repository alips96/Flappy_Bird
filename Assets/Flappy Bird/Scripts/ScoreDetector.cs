using Unity.Netcode;
using UnityEngine;

public class ScoreDetector : MonoBehaviour
{
    private GameObject gameManager;
    private EventManager eventMaster;
    private float birdXPos;
    private Transform myTransform;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");

        eventMaster = gameManager.GetComponent<EventManager>();
        birdXPos = gameManager.GetComponent<InstantiateBird>().xSpawnPos;

        myTransform = transform;
    }

    void Update()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            if (myTransform.position.x < birdXPos)
            {
                eventMaster.CallEventIncrementScore();
                Destroy(this);
            }
        }
    }
}
