using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private float playerHealth = 10f;
    [Tooltip("How often the player can get damaged")]
    [SerializeField]private float damageDelay = 1f;

    private float currentPlayerHealth;

    private bool isDead = false;
    private bool damaged = false;

    public void DamagePlayer(float damageAmount) {
        if (damageAmount > 5) {
            return;
        }
        else {
            currentPlayerHealth -= damageAmount;   
        }
    }

    public float GetHealth() {
        return playerHealth;
    }

    private void Awake() {
        currentPlayerHealth = 10f;
    }

    void Update() {
        if (currentPlayerHealth <= 0 && isDead == false) {
            isDead = true;
            Debug.Log("Cringe, you died :I");
        }
    }
}
