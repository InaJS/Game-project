using System;
using UnityEngine;

public class GoTowardsPlayer : MonoBehaviour {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float minDistance = 0.5f;
        
        public Transform target;

        private void Awake() {
              target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update() {
                if ((target.position - transform.position).magnitude < minDistance) {
                        return;
                } 
                
                float stepDistance = Time.deltaTime * moveSpeed;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, stepDistance);
        }
}
