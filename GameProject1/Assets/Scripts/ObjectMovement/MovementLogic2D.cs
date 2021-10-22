using System;
using UnityEngine;


[RequireComponent(typeof(Animator2DMovement))]
[RequireComponent(typeof(Rigidbody2D))]
public class MovementLogic2D : MonoBehaviour
{
    [SerializeField] private float speed;
    public Vector2 velocityDirection { get; set; }

    private Rigidbody2D body;
    private Animator2DMovement anim;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator2DMovement>();
    }

    private void Update()
    {
        body.velocity = velocityDirection * speed;
        anim.SetAnimatorFloats(velocityDirection);
    }
}