using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyWaves")]
public class EnemyWave : ScriptableObject
{
    public List<SpawnInfo> enemies;
}

public enum SpawnPosition
{
    left,down,right
}

[Serializable] public class SpawnInfo
{
    public EnemyAI Enemy;
    public SpawnPosition Position;
}