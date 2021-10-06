using System;
using UnityEngine;
using System.Collections.Generic;


public class GoTowardsPlayer : MonoBehaviour {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float minDistance = 0.5f;

        private Animator myAnim;
        public Transform target;

        private void Awake() {
              target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update() {
                if ((target.position - transform.position).magnitude < minDistance) {
                        return;
                } 
                myAnim.SetFloat("moveX", (target.position - transform.position).normalized.x);
                myAnim.SetFloat("moveY", Mathf.Sign(target.position.y - transform.position.y));
                float stepDistance = Time.deltaTime * moveSpeed;
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, stepDistance);
        }
}
