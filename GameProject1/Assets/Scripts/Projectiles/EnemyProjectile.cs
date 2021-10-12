using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5;
    [SerializeField] private float damage = 1;
    void Update() {
        lifeTime -= Time.deltaTime;
        if ( lifeTime < 0 )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerHealth.Instance.TryDamagePlayer(damage);
        }
    }
}
