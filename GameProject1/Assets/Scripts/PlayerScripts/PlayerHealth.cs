using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    [SerializeField] private float playerHealth = 10f;

    [Tooltip("How often the player can get damaged")] [SerializeField]
    private float damageDelay = 1f;

    [SerializeField] private UnityEvent onDeath;

    private float currentPlayerHealth;
    
    public void DamagePlayer(float damageAmount)
    {
        currentPlayerHealth -= damageAmount;
    }

    public float GetHealth()
    {
        return playerHealth;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        currentPlayerHealth = 10f;
    }

    void Update()
    {
        if (currentPlayerHealth <= 0)
        {
            onDeath.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (!enemy)
            {
                return;
            }
            int damage = (int) enemy.GetDamage();
            this.DamagePlayer(damage);
            // attack = StartCoroutine(attackTimer());
        }
    }
}