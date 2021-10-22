using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float firingRate;
    [SerializeField] private PlayerProjectileInstance[] shootEffectPrefabs;
    [SerializeField] private UnityEvent onBulletFired;
    
    [HideInInspector] public Vector2 firingDirection;
    private float firingCooldown => 1.0f / firingRate;
    private float timer;
    private bool inputAvailable;

    private void Update()
    {
        if (timer < 0)
        {
            inputAvailable = true;
            return;
        }
        
        timer -= Time.deltaTime;
    }

    public void FireBullet(Vector2 direction)
    {
        if (!inputAvailable)
        {
            return;
        }
        
        direction.Normalize();
        
        PlayerProjectileInstance bulletPrefab = shootEffectPrefabs[Random.Range(0, shootEffectPrefabs.Length)];
        PlayerProjectileInstance playerProjectile = Instantiate(bulletPrefab);

        playerProjectile.transform.position = this.transform.position;
        playerProjectile.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        
        onBulletFired.Invoke();
        inputAvailable = false;
        timer = firingCooldown;
    }

    public void FireBullet()
    {
        FireBullet(firingDirection);
    }
}
