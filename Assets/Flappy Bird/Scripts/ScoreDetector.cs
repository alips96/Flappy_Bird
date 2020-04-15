using System;
using UnityEngine;

public class ScoreDetector : MonoBehaviour
{
    private const float birdxPostition = -1.3f;
    private EventManager eventMaster;
    private GameObject gameManager;

    // Start is called before the first frame update
    private void Start()
    {
        eventMaster = GameObject.Find("GameManager").GetComponent<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < birdxPostition)
        {
            eventMaster.CallEventIncrementScore();
            Destroy(this);
        }
    }
}
