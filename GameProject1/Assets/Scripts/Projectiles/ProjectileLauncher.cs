using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate = 10f;
    [SerializeField] private ProjectileInstance[] shootEffectPrefabs;
    [SerializeField] private UnityEvent onBulletFired;
    
    private float firingTimer;

    void Start()
    {
        firingTimer = 1.0f / fireRate;
    }

    void Update()
    {
        firingTimer -= Time.deltaTime;
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        Vector3 direction = (target - this.transform.position).normalized;

        if (Input.GetMouseButton(0))
        {
            if (firingTimer < 0)
            {
                FireBullet(direction);
                firingTimer = 1.0f / fireRate;
            }
        }
    }

    void FireBullet(Vector2 normalizedDirection)
    {
        ProjectileInstance bulletPrefab = shootEffectPrefabs[Random.Range(0, shootEffectPrefabs.Length)];
        
        ProjectileInstance b = Instantiate(bulletPrefab);
        b.transform.position = transform.position;
        b.GetComponent<Rigidbody2D>().velocity = normalizedDirection * bulletSpeed;
        onBulletFired.Invoke();
    }
}
