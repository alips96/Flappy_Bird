using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    private EventManager eventManager;
    private int score;
    public Text scoreNomText;

    private void OnEnable()
    {
        SetInitialReferences();
        eventManager.EventIncrementScore += IncrementScore;
    }

    private void OnDisable()
    {
        eventManager.EventIncrementScore -= IncrementScore;
    }

    private void SetInitialReferences()
    {
        eventManager = GetComponent<EventManager>();
    }

    private void IncrementScore()
    {
        score++;

        //SetUI
        scoreNomText.text = "Score: " + score.ToString();
    }
}