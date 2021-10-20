using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private bool enableGizmo;
    [SerializeField] private SpawnZone[] spawnZones;
    [SerializeField] private List<SongWaves> songWaves;
    [SerializeField] private UnityEvent onNewSong;

    private List<EnemyAI> enemiesAlive = new List<EnemyAI>();
    private int currentWave = 0;
    private int currentSong = 0;
    
    public List<SongWaves> SongWaves => songWaves;

    private void Awake()
    {
        currentWave = 0;
        currentSong = 0;
        enemiesAlive.Clear();
    }

    private void Update()
    {
        if (enemiesAlive.Count > 0)
        {
            return;
        }

        if (currentWave >= SongWaves[currentSong].EnemyWaves.Count)
        {
            currentSong++;
            
            if (currentSong >= songWaves.Count)
            {
                Debug.Log("Victory");
                SceneManager.LoadScene("4_WinScreen");
                return;
            }
            
            currentWave = 0;
            onNewSong.Invoke();
            return;
        }

        SpawnWave(SongWaves[currentSong].EnemyWaves[currentWave]);
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