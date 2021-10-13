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
    [SerializeField] private UnityEvent onPlayerHeal;
    [SerializeField] private UnityEvent onDeath;

    private float currentPlayerHealth;
    private float damageTimer = -1;

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
        onPlayerHeal.Invoke();
        currentPlayerHealth += healValue;
        currentPlayerHealth = Mathf.Clamp(currentPlayerHealth, 0, playerHealth);

        float value = currentPlayerHealth / playerHealth;
        Color adjustedColor = Color.Lerp(LowHealth, FullHealth,value);
        coneRenderer.color = adjustedColor;
    }

    void DamagePlayer(float damageAmount)
    {
        onPlayerHit.Invoke();
        currentPlayerHealth -= damageAmount;
        damageTimer = damageDelay;

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
}