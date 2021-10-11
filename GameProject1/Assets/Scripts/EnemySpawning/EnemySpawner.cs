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
    [SerializeField] private List<EnemyWave> enemyWaveList;
    [SerializeField] private List<EnemyAI> enemiesAlive;

    public List<EnemyWave> EnemyWaveList => enemyWaveList;
    private int currentWave = 0;

    private void Awake()
    {
        NewSong();
        enemiesAlive.Clear();
    }

    public void NewSong()
    {
        currentWave = 0;
    }

    private void Update()
    {
        if (enemiesAlive.Count > 0)
        {
            return;
        }
        
        SpawnWave(enemyWaveList[currentWave]);

        currentWave++;
    }

    private void SpawnWave(EnemyWave wave)
    {
        foreach (SpawnInfo spawnInfo in wave.Enemies)
        {
            EnemyAI enemy = Spawn(spawnInfo.Enemy, spawnZones[spawnInfo.Position-1]);
            enemiesAlive.Add(enemy);
            enemy.onDeath += () => enemiesAlive.Remove(enemy);
        }
    }

    private EnemyAI Spawn(EnemyAI enemy, SpawnZone zone)
    {
        Vector3 spawnPosition = new Vector3();
        spawnPosition.x = Random.Range(zone.Center.x - zone.Width / 2, zone.Center.x + zone.Width / 2);
        spawnPosition.y = Random.Range(zone.Center.y - zone.Height / 2, zone.Center.y + zone.Height / 2);

        return Instantiate(enemy, spawnPosition, Quaternion.identity);
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