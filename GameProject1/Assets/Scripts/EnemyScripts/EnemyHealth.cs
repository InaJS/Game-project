using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int enemyHealth = 5;

    void Update() {
        // I removed the death check from here,
        // because we only need to check for death when the enemy takes damage ;)
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Projectile")) {
            enemyHealth--;
            if (enemyHealth <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
