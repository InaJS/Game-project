using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float startTimeBetweenShots;
    [SerializeField] private EnemyProjectile[] ShootEffectPrefabs;

    private float timeBetweenShots;
    private Transform player;

    void Start()
    {
        player = PlayerHealth.Instance.gameObject.transform;

        timeBetweenShots = startTimeBetweenShots;
    }

    void Update()
    {
        if (timeBetweenShots <= 0)
        {
            Vector3 direction = (player.position - this.transform.position).normalized;

            FireBullet(direction);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }

    void FireBullet(Vector2 direction)
    {
        EnemyProjectile bulletPrefab = ShootEffectPrefabs[Random.Range(0, ShootEffectPrefabs.Length)];

        EnemyProjectile b = Instantiate(bulletPrefab);
        b.transform.position = transform.position;
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}