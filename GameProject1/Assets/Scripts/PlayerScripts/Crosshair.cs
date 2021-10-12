using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Crosshair : MonoBehaviour
    {
        private Animator animator;
        private static Crosshair instance;

        public static Crosshair Instance
        {
            get
            {
                instance = FindObjectOfType<Crosshair>();
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<Crosshair>();
                }

                return instance;
            }
        }

        public void ResetFireTrigger()
        {
            animator.ResetTrigger("Fire");
        }

        public void SetFireAnimationTrigger()
        {
            animator.SetTrigger("Fire");
        }

        private void Awake()
        {
            animator = this.GetComponent<Animator>();
            if (instance != null)
            {
                Destroy(this);
            }
        }
    }
}