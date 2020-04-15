using UnityEngine;

public class GameOver : MonoBehaviour
{
    private EventManager gameOverScript;

    private void Start()
    {
        gameOverScript = GameObject.Find("GameManager").GetComponent<EventManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Time.timeScale = 0;
        //LoadLevelCanvas.SetActive(true);
        gameOverScript.CallEventGameOver();
    }

    //public void ReplayButtonClicked() //called when replay button clicked.
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    LoadLevelCanvas.SetActive(false);
    //    Time.timeScale = 1;
    //}
}
