using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileLifeTime : MonoBehaviour {
    public float lifeTime = 5;
    void Update() {
        lifeTime -= Time.deltaTime;
        if ( lifeTime < 0 )
        {
            Destroy(gameObject);
        }
    }
}
