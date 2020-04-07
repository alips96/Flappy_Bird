using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    private ScoreMaster scoreMaster;
    private int score;
    private int highScore;
    public Text scoreNomText;
    public Text highScoreText;

    private void OnEnable()
    {
        SetInitialReferences();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();

        scoreMaster.EventIncrementScore += IncrementScore;
    }

    private void OnDisable()
    {
        scoreMaster.EventIncrementScore -= IncrementScore;
    }

    private void SetInitialReferences()
    {
        scoreMaster = GetComponent<ScoreMaster>();
    }

    private void IncrementScore()
    {
        score++;

        //SetUI
        scoreNomText.text = "Score: " + score.ToString();

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }
}