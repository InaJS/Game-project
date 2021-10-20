using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemySpawn/EnemyWaves")]
public class EnemyWave : ScriptableObject
{
    public List<SpawnInfo> Enemies;
}
[Serializable] 
public class SpawnInfo
{
    [Tooltip("The enemy prefab to spawn")]
    public EnemyAI Enemy;
    [Tooltip("The zone in which it will spawn (1 = left, 2 = down, 3 = right)")]
    [Range(1,3)] public int Position = 1;
}