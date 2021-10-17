using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstance : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject, 0.02f);
        }
    }
}