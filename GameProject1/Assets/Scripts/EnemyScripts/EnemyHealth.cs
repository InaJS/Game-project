using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int enemyHealth = 5;

    void Update() {
        if (enemyHealth <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            //StartCoroutine(EnemyOnCollision.attack);
        }
        if (collision.gameObject.CompareTag("Projectile")) {
            enemyHealth--;   
        }
    }
}
