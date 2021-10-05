using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private List <EnemyOnCollision> enemyList = new List <EnemyOnCollision>();
    private float spawnTime = 3f;
    private Vector3 spawnPosition;
    private bool enableGizmo;

    private void Start() {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn() {
        spawnPosition.x = Random.Range(-17, 17);
        spawnPosition.y = Random.Range(-17, 17);
        Instantiate(enemyList[Random.Range(0, enemyList.Count)], spawnPosition, Quaternion.identity);
    }

    private void OnDrawGizmos() {
        if (!enableGizmo)
        {
            return;
        }
        
    }
}
