using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5;
    [SerializeField] private float damage = 1;
    void Update() {
        lifeTime -= Time.deltaTime;
        if ( lifeTime < 0 )
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) 
        {
            PlayerHealth.Instance.TryDamagePlayer(damage);
            Destroy(this.gameObject,0.02f);
        }
    }
}
