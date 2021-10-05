using UnityEngine;

public class GoTowardsPlayer : MonoBehaviour {
        [SerializeField] private float moveSpeed = 2f;
        
        public Transform target;
        void Update() {
                transform.LookAt(target.position);
                transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
        }
}
