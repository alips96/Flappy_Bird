using Unity.Netcode;
using UnityEngine;

public class BirdController : NetworkBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D myRigidbody;

    void Start() => myRigidbody = GetComponent<Rigidbody2D>();

    public void MoveBird() //Called by the PlayerInput Script
    {
        myRigidbody.linearVelocity = Vector2.up * speed;
    }
}
