using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]private float moveSpeed;
    
    private float horizontal;
    private float vertical;
    
    public Rigidbody2D playerBody;
    
    
    
    void Start() {
        playerBody = GetComponent <Rigidbody2D>();
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        playerBody.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }
}
