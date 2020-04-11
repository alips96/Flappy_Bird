﻿using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class Bird : MonoBehaviourPun
{
    public float speed;
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
                myRigidbody.velocity = Vector2.up * speed;
            }
        }
    }
}
