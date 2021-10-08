using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    [Tooltip("How often the player will take damage")]
    [SerializeField] private float damageDelay = 1f;
    [SerializeField] private float damageAmount;
    [SerializeField] private int enemyHealth = 5;

    private Coroutine attack;
    private float time = 1f;
    private PlayerHealth player;


    private void Awake() {
        // I changed the check for damage to be a responsibility of the player scripts
        // old code for reference
        // playerHealth = GameObject.Find("Player").GetComponent <PlayerHealth>();
    }

    public float GetDamage()
    {
        return damageAmount;
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        // I moved some of the stuff from the enemy health script into this one, because there were some
        // repeated trigger-enter checks
        if (collision.gameObject.CompareTag("Projectile")) {
            enemyHealth--;
            if (enemyHealth <= 0) {
                Destroy(gameObject);
            }
        }
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


    // private void OnTriggerExit2D(Collider2D other) {
    //     if (other.gameObject.CompareTag("Player")) {
    //         StopCoroutine(attack);
    //     }
    // }
}
