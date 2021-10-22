using System;
using PlayerScripts;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Animator2DMovement))]
[RequireComponent(typeof(MovementLogic2D))]
public class PlayerMovementInput : MonoBehaviour
{
    [HideInInspector] [SerializeField] private UnityEvent<Vector2> onMovement;
    private MovementLogic2D _movementLogic2D;
    private Animator2DMovement animator2DMovement;
    private Vector2 movementInput;

    private void Awake()
    {
        _movementLogic2D = GetComponent<MovementLogic2D>();
        animator2DMovement = GetComponent<Animator2DMovement>();
        onMovement.AddListener((movementInput) => _movementLogic2D.velocityDirection = movementInput);
        onMovement.AddListener(animator2DMovement.SetAnimatorFloats);
    }

    private void Update()
    {
        if (Time.timeScale < 0.1)
        {
            return;
        }

        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        onMovement.Invoke(movementInput.normalized);
    }
}