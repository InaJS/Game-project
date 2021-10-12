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
    [SerializeField] private Color FullHealth;
    [SerializeField] private Color LowHealth;

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

    public void HealPlayer(float healValue)
    {
        DamagePlayer(-healValue);
    }

    void DamagePlayer(float damageAmount)
    {
        onPlayerHit.Invoke();
        currentPlayerHealth -= damageAmount;

        float value = currentPlayerHealth / playerHealth;
        Color adjustedColor = Color.Lerp(LowHealth, FullHealth,value);
        coneRenderer.color = adjustedColor;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        coneRenderer.color = FullHealth;

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