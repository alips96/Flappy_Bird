using Unity.Netcode;
using UnityEngine;

public class BirdController : NetworkBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D myRigidbody;

    void Start() => myRigidbody = GetComponent<Rigidbody2D>();

    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && !IsOwner)
        //{
        //    NetworkObject.ChangeOwnership(NetworkManager.Singleton.LocalClientId);
        //}

        //if (IsOwner && Input.GetMouseButtonDown(0))
        //{
        //    myRigidbody.linearVelocity = Vector2.up * speed;
        //}

        if (Input.GetMouseButtonDown(0))
        {
            myRigidbody.linearVelocity = Vector2.up * speed;
        }
    }
}
