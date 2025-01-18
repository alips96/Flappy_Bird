using Unity.Netcode;
using UnityEngine;
using UniRx;

public class ScoreDetector : MonoBehaviour
{
    private EventManager eventMaster;
    private readonly ReactiveProperty<float> birdXPos = new();

    private void Start()
    {
        if (!NetworkManager.Singleton.IsServer) return;

        var gameManager = GameObject.Find("GameManager");
        if (gameManager == null) return;

        eventMaster = gameManager.GetComponent<EventManager>();

        birdXPos.Value = gameManager.GetComponent<InstantiateBird>().xSpawnPos;

        birdXPos
            .Where(_ => transform.position.x < birdXPos.Value)
            .Take(1) // trigger only once
            .Subscribe(_ =>
            {
                eventMaster.CallEventIncrementScore();
                Destroy(this);
            })
            .AddTo(this);
    }
}
