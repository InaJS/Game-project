using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private bool enableGizmo;
    [SerializeField] private SpawnZone[] spawnZones;
    // [SerializeField] private float spawnTime = 3f;
    [SerializeField] private List<EnemyWave> enemyWaveList;
    
    public List<EnemyWave> EnemyWaveList => enemyWaveList;

    private void Start()
    {
        // InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    public void SpawnOnZone(SpawnZone zone)
    {
        EnemyWave randomWave = enemyWaveList[Random.Range(0, enemyWaveList.Count)];
        EnemyAI randomEnemy = randomWave.Enemies[Random.Range(0, enemyWaveList.Count)].Enemy;

        Spawn(randomEnemy, zone);
    }

    void Spawn(EnemyAI enemy, SpawnZone zone)
    {
        Vector3 spawnPosition = new Vector3();
        spawnPosition.x = Random.Range(zone.Center.x - zone.Width / 2, zone.Center.x + zone.Width / 2);
        spawnPosition.y = Random.Range(zone.Center.y - zone.Height / 2, zone.Center.y + zone.Height / 2);

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
    [Range(0f, 50f)] public float Width;
    [Range(0f, 50f)] public float Height;
}