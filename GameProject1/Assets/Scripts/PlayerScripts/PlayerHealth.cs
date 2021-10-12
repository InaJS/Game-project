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
    [SerializeField] private SpriteRenderer coneRenderer;

    [Tooltip("How often the player can get damaged")] 
    [SerializeField] private float damageDelay = 1f;

    [SerializeField] private UnityEvent onPlayerHit;
    [SerializeField] private UnityEvent onDeath;

    private float currentPlayerHealth;
    private float damageTimer = 0;

    public void TryDamagePlayer(float damageAmount)
    {
        if (damageTimer > 0)
        {
            return;
        }
        
        DamagePlayer(damageAmount);
    }

    void DamagePlayer(float damageAmount)
    {
        onPlayerHit.Invoke();

        Color adjustedColor = coneRenderer.color;
        adjustedColor.a = currentPlayerHealth / playerHealth;
        coneRenderer.color = adjustedColor;
        currentPlayerHealth -= damageAmount;
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

        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (damageTimer > 0)
        {
            return;
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();
            if (!enemy)
            {
                return;
            }

            damageTimer = damageDelay;

            int damage = (int) enemy.GetDamage();
            DamagePlayer(damage);
        }
    }
}