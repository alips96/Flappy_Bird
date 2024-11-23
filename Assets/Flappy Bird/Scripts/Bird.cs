using Photon.Pun;
using UnityEngine;

public class Bird : MonoBehaviourPun
{
    [SerializeField] private float speed;
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start() => myRigidbody = GetComponent<Rigidbody2D>();

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //flap
                myRigidbody.linearVelocity = Vector2.up * speed;
            }
        }
    }
}
