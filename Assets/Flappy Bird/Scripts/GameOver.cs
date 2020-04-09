using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject LoadLevelCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
        LoadLevelCanvas.SetActive(true);
    }

    public void ReplayButtonClicked() //called when replay button clicked.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LoadLevelCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
