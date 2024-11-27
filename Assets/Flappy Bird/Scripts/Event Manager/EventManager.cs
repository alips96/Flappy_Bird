using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventIncrementScore;

    public delegate void GameOverEventHandler(ulong loserClientId);
    public event GameOverEventHandler EventGameOver;

    public void CallEventGameOver(ulong ownerClientId)
    {
        EventGameOver?.Invoke(ownerClientId);
    }

    internal void CallEventIncrementScore()
    {
        EventIncrementScore.Invoke();
    }
}
