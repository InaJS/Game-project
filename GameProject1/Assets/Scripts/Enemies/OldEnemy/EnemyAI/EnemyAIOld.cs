using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using CustomEventSystem;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAIOld : MonoBehaviour 
{
    [SerializeField] private float damageAmount;
    [SerializeField] private int enemyHealth = 5;
    [SerializeField] private UnityEvent deathCallback;
    private GoTowardsPlayer movementAI;
    private EnemyFire shootBehavior;
    private Collider2D collider;

    public delegate void OnDeath();

    public OnDeath onDeath;


    private void Awake()
    {
        movementAI = this.GetComponent<GoTowardsPlayer>();
        shootBehavior = this.GetComponent<EnemyFire>();
        collider = this.GetComponent<Collider2D>();
        
        onDeath += () => deathCallback.Invoke();
        onDeath += () => movementAI.enabled = false;
        onDeath += () => collider.enabled = false;

        if (shootBehavior != null)
        {
            onDeath += () => shootBehavior.enabled = false;
        }
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
}
