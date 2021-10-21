using System;
using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementLogic : MonoBehaviour
    {
        [SerializeField] private float playerSpeed;
        public Vector2 playerVelocityDirection
        {
            get;
            set;
        }
        
        private Rigidbody2D body;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            body.velocity = playerVelocityDirection * playerSpeed;
        }
    }
}