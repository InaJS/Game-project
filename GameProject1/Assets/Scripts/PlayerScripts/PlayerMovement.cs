using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator animator;
    
    private float horizontal;
    private float vertical;
    private Rigidbody2D playerBody;

    public Vector2 PlayerInput
    {
        get => new Vector2(horizontal, vertical);
    }

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        //The animations for movement in different directions uses the first two directly underneath.
        animator.SetFloat("XSpeed", horizontal);
        animator.SetFloat("YSpeed", vertical);
        //Do not touch!!
    }

    private void FixedUpdate()
    {
        playerBody.velocity = new Vector2(horizontal, vertical).normalized * moveSpeed;
        
        // I changed this a bit because the vector was not normalized, so diagonal movement was going faster,
        // old code for reference:
        // playerBody.velocity = new Vector2(horizontal* moveSpeed, vertical* moveSpeed);
    }
}