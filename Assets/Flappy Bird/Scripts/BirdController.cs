using Unity.Netcode;
using UnityEngine;

public class BirdController : NetworkBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D myRigidbody;

    void Start() => myRigidbody = GetComponent<Rigidbody2D>();

    void Update()
    {
        if (!IsOwner)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            //flap
            myRigidbody.linearVelocity = Vector2.up * speed;
        }
    }
}
