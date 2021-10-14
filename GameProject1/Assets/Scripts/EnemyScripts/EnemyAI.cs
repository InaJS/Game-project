using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using CustomEventSystem;
using UnityEngine;

public class EnemyAI : MonoBehaviour 
{
    [SerializeField] private float damageAmount;
    [SerializeField] private int enemyHealth = 5;

    public delegate void OnDeath();

    public OnDeath onDeath;


    private void Awake() 
    {
        onDeath += () => Destroy(gameObject);
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
