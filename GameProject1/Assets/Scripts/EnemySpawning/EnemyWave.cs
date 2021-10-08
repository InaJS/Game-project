using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyWaves")]
public class EnemyWave : ScriptableObject
{
    
    
}

public enum SpawnPosition
{
    left,down,right
}

public class SpawnInfo
{
    public EnemyAI Enemy;
    public SpawnPosition Position;
}