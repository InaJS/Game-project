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
    [SerializeField] private GameObject bulletStart;
    private Crosshair crosshairs;

    private float shootCooldown;
    private Vector3 target;

    void Start()
    {
        crosshairs = Crosshair.Instance;
        
        shootCooldown = 1.0f / fireRate;
        Cursor.visible = false;
    }

    void Update()
    {
        shootCooldown -= Time.deltaTime;
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        crosshairs.transform.position = new Vector2(target.x, target.y);

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
        crosshairs.SetFireAnimationTrigger();
        
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = bulletStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}