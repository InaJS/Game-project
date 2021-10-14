using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using CustomEventSystem;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour 
{
    [SerializeField] private float damageAmount;
    [SerializeField] private int enemyHealth = 5;
    [SerializeField] private UnityEvent deathCallback;
    [SerializeField] private GoTowardsPlayer movementAI;

    public delegate void OnDeath();

    public OnDeath onDeath;


    private void Awake()
    {
        movementAI = this.GetComponent<GoTowardsPlayer>();
        
        onDeath += () => Destroy(gameObject);
        onDeath += () => deathCallback.Invoke();
        onDeath += () => movementAI.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Projectile")) 
        {
            enemyHealth--;
            if (enemyHealth <= 0) {
                onDeath.Invoke();
            }
            
            return;
        }
        
        if (collision.gameObject.CompareTag("Player")) 
        {
            PlayerHealth.Instance.TryDamagePlayer(damageAmount);
        }
    }

    // private void OnCollisionStay2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Player")) 
    //     {
    //         PlayerHealth.Instance.TryDamagePlayer(damageAmount);
    //     }
    // }
}
