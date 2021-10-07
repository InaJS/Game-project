using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Crosshair : MonoBehaviour
    {
        private SpriteRenderer renderer; 
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
        
        protected virtual void Awake()
        {
            renderer = this.GetComponent<SpriteRenderer>();
            if(instance != null) Destroy(this);
            DontDestroyOnLoad(this);
        }
    }
}