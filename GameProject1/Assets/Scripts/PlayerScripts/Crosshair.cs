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

        private void Update()
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            transform.position = new Vector2(target.x, target.y);
        }
    }
}