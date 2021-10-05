using UnityEngine;

public class GoTowardsPlayer : MonoBehaviour {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float minDistance = 0.1f;
        
        public Transform target;
        void Update() {
                if ((target.position - transform.position).magnitude < minDistance) {
                        return;
                }
                Vector3 movementDirection = (target.position - transform.position).normalized;
                transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
        }
}
