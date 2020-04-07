using UnityEngine;

public class ScoreMaster : MonoBehaviour
{
    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventIncrementScore;

    public void CallEventIncrementScore()
    {
        EventIncrementScore.Invoke();
    }
}
