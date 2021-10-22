using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float startTimeBetweenShots;
    [SerializeField] private EnemyProjectile[] ShootEffectPrefabs;
    [SerializeField] private AudioSource audio;
    private PushableObject pushable;

    private float timeBetweenShots;
    private Transform player;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        player = PlayerHealth.Instance.gameObject.transform;
        pushable = GetComponent<PushableObject>();

        float startTime = Random.Range(0, startTimeBetweenShots);

        timeBetweenShots = startTime;
    }

    void Update()
    {
        if (timeBetweenShots > 0)
        {
            timeBetweenShots -= Time.deltaTime;
        }
        else
        {
            if (pushable.isBeingPushed)
            {
                return;
            }
            
            Vector3 direction = (player.position - this.transform.position).normalized;

            FireBullet(direction);
            timeBetweenShots = startTimeBetweenShots;
        }
    }

    void FireBullet(Vector2 direction)
    {
        EnemyProjectile bulletPrefab = ShootEffectPrefabs[Random.Range(0, ShootEffectPrefabs.Length)];

        EnemyProjectile b = Instantiate(bulletPrefab);
        b.transform.position = transform.position;
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        
        audio.Play();
    }
}