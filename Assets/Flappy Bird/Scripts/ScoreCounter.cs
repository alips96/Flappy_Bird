using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : NetworkBehaviour
{
    private EventManager eventManager;
    [SerializeField] private Text scoreNomText;

    private NetworkVariable<int> score = new();

    private void OnEnable()
    {
        SetInitialReferences();
        eventManager.EventIncrementScore += IncrementScore;
    }

    private void OnDisable()
    {
        eventManager.EventIncrementScore -= IncrementScore;
    }

    public override void OnNetworkSpawn()
    {
        score.OnValueChanged += UpdateScoreUI;
    }

    private void SetInitialReferences()
    {
        eventManager = GetComponent<EventManager>();
    }

    private void IncrementScore()
    {
        score.Value++;
    }

    private void UpdateScoreUI(int previousValue, int newValue)
    {
        scoreNomText.text = "Score: " + score.Value.ToString();
    }
}