using UnityEngine;
using Photon.Pun;

public class GameOverDetection : MonoBehaviour
{
    private EventManager gameOverScript;

    private void Start()
    {
        gameOverScript = GameObject.Find("GameManager").GetComponent<EventManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameOverScript.CallEventGameOver();
    }
}
