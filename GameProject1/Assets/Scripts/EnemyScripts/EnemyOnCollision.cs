using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnCollision : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("Projectile")) {
                Debug.Log("Enemy has been Hit!");
            }

            if (collision.gameObject.CompareTag("Player")) {
                Debug.Log("Player has been hit!");
            }
    }
}
