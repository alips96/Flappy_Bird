using UnityEngine;
using UniRx;

public class MoveLeft : MonoBehaviour
{
    [SerializeField] private float speed;
    private float groundSize;

    private void Start()
    {
        if (CompareTag("Ground"))
        {
            groundSize = GetComponentInChildren<BoxCollider2D>().size.x;
        }

        Observable.EveryUpdate()
            .Subscribe(_ => MoveObject())
            .AddTo(this);
    }

    private void MoveObject()
    {
        transform.position += Vector3.left * (speed * Time.deltaTime);

        if (groundSize > 0 && transform.position.x < -groundSize)
        {
            transform.position += Vector3.right * groundSize;
        }
    }
}
