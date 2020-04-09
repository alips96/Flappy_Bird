using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    private float groundSize;

    // Start is called before the first frame update
    private void Start()
    {
        if(gameObject.tag.CompareTo("Ground") == 0)
        {
            groundSize = GetComponentInChildren<BoxCollider2D>().size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - (speed * Time.deltaTime), transform.position.y);

        if(groundSize != 0)
        {
            if (transform.position.x < -groundSize)
            {
                transform.position = new Vector2(transform.position.x + groundSize, transform.position.y);
            }
        }

    }
}
