using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    [Tooltip("How often the player will take damage")]
    [SerializeField] private float damageAmount;
    [SerializeField] private int enemyHealth = 5;

    private Coroutine attack;
    private float time = 1f;
    private PlayerHealth player;
    
    public delegate void OnDeath();

    public OnDeath onDeath;


    private void Awake() {
        onDeath += () => Destroy(gameObject);
    }
    
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Projectile")) {
            enemyHealth--;
            if (enemyHealth <= 0) {
                onDeath.Invoke();
            }
        }
        
        if (collision.gameObject.CompareTag("Player")) 
        {
            PlayerHealth.Instance.TryDamagePlayer(damageAmount);
        }
    }
}
