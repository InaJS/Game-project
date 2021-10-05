using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileController : MonoBehaviour {
    public float lifeTime = 5;
    void Update() {
        lifeTime -= Time.deltaTime;
        if ( lifeTime < 0 )
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Destroy(gameObject);
        }
    }
}