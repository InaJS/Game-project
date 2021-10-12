using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using Unity.Mathematics;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate = 10f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ProjectileInstance[] shootEffectPrefabs;
    [SerializeField] private GameObject bulletStart;

    private float shootCooldown;
    private Vector3 target;

    void Start()
    {
        shootCooldown = 1.0f / fireRate;
    }

    void Update()
    {
        shootCooldown -= Time.deltaTime;
        Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        Vector3 difference = target - this.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (Input.GetMouseButton(0))
        {
            if (shootCooldown < 0)
            {
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                fireBullet(direction, rotationZ);
                shootCooldown = 1.0f / fireRate;
            }
        }
    }

    void fireBullet(Vector2 direction, float rotationZ)
    {
        // crosshairs.SetFireAnimationTrigger();
        
        ProjectileInstance bulletPrefab = shootEffectPrefabs[Random.Range(0, shootEffectPrefabs.Length)];
        
        ProjectileInstance b = Instantiate(bulletPrefab);
        b.transform.position = bulletStart.transform.position;
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}