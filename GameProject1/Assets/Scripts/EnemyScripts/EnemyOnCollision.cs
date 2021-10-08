using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

public class EnemyOnCollision : MonoBehaviour {
    [Tooltip("How often the player will take damage")]
    [SerializeField] private float damageDelay = 1f;
    [SerializeField] private float damageAmount;

    private Coroutine attack;
    private float time = 1f;
    private PlayerHealth playerHealth;

    private void Awake() {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        
        // I believe you werent familiar with "find of type"?
        // I changed this mostly because the readability was bad, but also because "find of type" is probably faster 
        // old code for reference
        // playerHealth = GameObject.Find("Player").GetComponent <PlayerHealth>();
    }
    
    // private IEnumerator attackTimer() {
    //     while (true) {
    //         if (playerHealth.GetHealth() <= 0) {
    //             break;
    //         }
    //
    //         Debug.Log("Player has been hit!");
    //         playerHealth.DamagePlayer(1);
    //         yield return new WaitForSeconds(time);
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Projectile")) {
                // Debug.Log("Enemy has been Hit!");
        }
        
        if (collision.gameObject.CompareTag("Player")) {
                // Debug.Log("Player has been hit!");
                collision.gameObject.GetComponent<PlayerHealth>().DamagePlayer(damageAmount);
                // attack = StartCoroutine(attackTimer());
        }
    }

    // private void OnTriggerExit2D(Collider2D other) {
    //     if (other.gameObject.CompareTag("Player")) {
    //         StopCoroutine(attack);
    //     }
    // }
}
