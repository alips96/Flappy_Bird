using UnityEngine;

public class Bird : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start() => myRigidbody = GetComponent<Rigidbody2D>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //flap
            myRigidbody.velocity = Vector2.up * speed;
        }
    }
}
