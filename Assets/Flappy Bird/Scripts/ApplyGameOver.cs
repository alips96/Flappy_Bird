using UnityEngine;
using UnityEngine.UI;

public class ApplyGameOver : MonoBehaviour
{
    [SerializeField] private GameObject loadLevelCanvas;
    private Text victoryDialog;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text playerNames;
    [SerializeField] private Text otherScoreText;
    [SerializeField] private Text otherplayerNames;

    private EventManager eventManager;

    private void OnEnable()
    {
        SetInitialReferences();

        eventManager.EventGameOver += PerformGameOver;
    }

    private void SetInitialReferences()
    {
        eventManager = GameObject.Find("GameManager").GetComponent<EventManager>();
    }

    private void OnDisable()
    {
        eventManager.EventGameOver -= PerformGameOver;
    }

    private void PerformGameOver()
    {
        FreezeGame();
        SetNamesAndScore();
        loadLevelCanvas.SetActive(true);
        victoryDialog = loadLevelCanvas.GetComponentInChildren<Text>();

        victoryDialog.color = Color.red;
        loadLevelCanvas.GetComponentInChildren<Text>().text = "You lost :(";
    }

    private void SetNamesAndScore()
    {
        ScoreText.text = otherScoreText.text;
        playerNames.text = otherplayerNames.text;
    }

    private void FreezeGame()
    {
        MoveLeft groundMovement = GameObject.Find("Ground").GetComponent<MoveLeft>();
        ColumnSpawner columnSpawner = GetComponent<ColumnSpawner>();

        groundMovement.enabled = false;
        columnSpawner.enabled = false;

        GameObject[] columns = GameObject.FindGameObjectsWithTag("Column");
        GameObject bird = GameObject.FindGameObjectWithTag("Player");
        Destroy(bird);

        foreach (GameObject item in columns)
        {
            Destroy(item);
        }
    }
}
