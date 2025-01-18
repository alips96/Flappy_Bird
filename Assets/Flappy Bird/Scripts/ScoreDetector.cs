using Unity.Netcode;
using UnityEngine;
using UniRx;

public class ScoreDetector : MonoBehaviour
{
    private EventManager eventMaster;
    [SerializeField] private float birdXPos;

    private void Start()
    {
        if (!NetworkManager.Singleton.IsServer) return;

        var gameManager = GameObject.Find("GameManager");
        if (gameManager == null) return;

        eventMaster = gameManager.GetComponent<EventManager>();
        birdXPos = gameManager.GetComponent<InstantiateBird>().xSpawnPos;

        Observable.EveryUpdate()
            .Where(_ => transform.position.x < birdXPos)
            .Take(1) // trigger only once
            .Subscribe(_ =>
            {
                eventMaster.CallEventIncrementScore();
                Destroy(this);
            })
            .AddTo(this);
    }
}
