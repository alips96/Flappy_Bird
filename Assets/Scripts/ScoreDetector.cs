using UnityEngine;

public class ScoreDetector : MonoBehaviour
{
    float birdPostition;
    ScoreMaster scoreMaster;

    // Start is called before the first frame update
    private void Start()
    {
        birdPostition = GameObject.Find("Bird").transform.position.x;
        scoreMaster = GameObject.Find("GameManager").GetComponent<ScoreMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < birdPostition)
        {
            scoreMaster.CallEventIncrementScore();
            Destroy(this);
        }
    }
}
