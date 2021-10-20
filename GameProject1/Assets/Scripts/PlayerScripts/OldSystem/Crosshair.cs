using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PlayerScripts
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private Texture2D cursorTexture;
        private Animator animator;
        private Camera camera;
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
                Destroy(instance.gameObject);
                instance = this;
            }
            
            DontDestroyOnLoad(this.gameObject);
            Cursor.SetCursor(cursorTexture, -Vector2.one*0.5f, CursorMode.Auto);
        }

        private void Update()
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(target.x, target.y,0);
        }
    }
}