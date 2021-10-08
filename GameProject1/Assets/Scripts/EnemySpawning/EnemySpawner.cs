using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyWave> enemyWaveList;
    [SerializeField] private bool enableGizmo;
    [SerializeField] private List<SpawnZone> spawnZones;
    
    [Range(0f, 25f)] [SerializeField] private float cubeHeight = 5f;
    [Range(0f, 25f)] [SerializeField] private float cubeWidth = 5f;
    [SerializeField] private float spawnTime = 3f;

    private void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    public void SpawnOnZone(SpawnZone zone)
    {
        EnemyWave randomWave = enemyWaveList[Random.Range(0, enemyWaveList.Count)];
        EnemyAI randomEnemy = randomWave.enemies[Random.Range(0, enemyWaveList.Count)].Enemy;

        Spawn(randomEnemy, zone);
    }

    void Spawn(EnemyAI enemy, SpawnZone zone)
    {
        Vector3 spawnPosition = new Vector3();
        spawnPosition.x = Random.Range(transform.position.x - cubeWidth / 2, transform.position.x + cubeWidth / 2);
        spawnPosition.y = Random.Range(transform.position.y - cubeHeight / 2, transform.position.y + cubeHeight / 2);

        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
    
    private void OnDrawGizmos()
    {
        if (!enableGizmo)
        {
            return;
        }

        foreach (var zone in spawnZones)
        {
            var sizeOfCube = new Vector3(zone.Width, zone.Height);
            Gizmos.DrawWireCube(zone.Center, sizeOfCube);
        }
    }
}

[Serializable]
public class SpawnZone
{
    public Vector3 Center;
    [Range(0f, 25f)] public float Width;
    [Range(0f, 25f)] public float Height;
}