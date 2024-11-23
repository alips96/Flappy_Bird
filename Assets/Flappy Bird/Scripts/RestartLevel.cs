using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void ReloadCurrentLevel() //Called by Restart button UI
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GotoMainMenu() //Called by Main Menue button UI
    {
        SceneManager.LoadScene(0);
    }
}
