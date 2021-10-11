using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/EnemySpawn/Song")]
public class SongWaves : ScriptableObject
{
    public List<EnemyWave> waves;
}