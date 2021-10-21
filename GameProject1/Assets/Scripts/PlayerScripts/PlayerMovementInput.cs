using System;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerScripts
{
    [RequireComponent(typeof(PlayerMovementLogic))]
    public class PlayerMovementInput : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Vector2> onMovement;
        private PlayerMovementLogic movementLogic;
        private Vector2 movementInput;
        private void Awake()
        {
            movementLogic = GetComponent<PlayerMovementLogic>();
            onMovement.AddListener((movementInput) => movementLogic.playerVelocityDirection = movementInput);
        }

        private void Update()
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
            
            onMovement.Invoke(movementInput.normalized);
        }
    }
}