using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private PlayerProjectileInstance[] shootEffectPrefabs;
    [SerializeField] private UnityEvent onBulletFired;
    
    public void FireBullet(Vector2 direction)
    {
        direction.Normalize();
        
        PlayerProjectileInstance bulletPrefab = shootEffectPrefabs[Random.Range(0, shootEffectPrefabs.Length)];
        
        PlayerProjectileInstance playerProjectile = Instantiate(bulletPrefab);
        playerProjectile.transform.position = this.transform.position;
        playerProjectile.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        onBulletFired.Invoke();
    }
}
