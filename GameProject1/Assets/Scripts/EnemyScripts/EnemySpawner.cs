using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private List <EnemyOnCollision> enemyList = new List <EnemyOnCollision>();
    [SerializeField] private bool enableGizmo;
    [Range(0f, 25f)] [SerializeField] private float cubeHeight = 5f;
    [Range(0f, 25f)] [SerializeField] private float cubeWidth = 5f;
    private float spawnTime = 3f;
    private Vector3 spawnPosition;
    private Vector3 sizeOfCube;

    private void Start() {
        sizeOfCube = new Vector3(cubeWidth, cubeHeight);
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn() {
        spawnPosition.x = Random.Range(transform.position.x - cubeWidth / 2 , transform.position.x + cubeWidth / 2);
        spawnPosition.y = Random.Range(transform.position.y - cubeHeight /2 , transform.position.y + cubeHeight / 2);
        Instantiate(enemyList[Random.Range(0, enemyList.Count)], spawnPosition, Quaternion.identity);
    }

    private void OnValidate() {
        sizeOfCube = new Vector3(cubeWidth, cubeHeight);
    }

    private void OnDrawGizmos() {
        if (!enableGizmo)
        {
            return;
        }
        
        Gizmos.DrawWireCube(transform.position, sizeOfCube);
    }
}
