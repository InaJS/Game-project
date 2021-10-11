using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects, CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    public List<SongWaves> WaveListsPerSong;

    private void OnEnable()
    {
        EnemySpawner spawner = (EnemySpawner) target;
        WaveListsPerSong = spawner.SongWaves;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        for (int i = 0; i < WaveListsPerSong.Count; i++)
        {
            SongWaves song = WaveListsPerSong[i];
            
            if (song == null)
            {
                continue;
            }

            EditorGUILayout.LabelField(song.name);

            for (int j = 0; j < WaveListsPerSong[i].waves.Count; j++)
            {
                EnemyWave wave = WaveListsPerSong[i].waves[j];
                
                if (wave == null)
                {
                    continue;
                }

                EditorGUILayout.LabelField("    "+ wave.name);
                
                for (int k = 0; k < wave.Enemies.Count; k++)
                {
                    SpawnInfo spawnInfo = wave.Enemies[k];

                    if (spawnInfo == null || spawnInfo.Enemy == null)
                        continue;

                    var positionString = "";

                    positionString = spawnInfo.Position switch
                    {
                        1 => "left",
                        2 => "down",
                        3 => "right",
                        _ => positionString
                    };

                    GUILayout.Label("       Enemy " + k + ": " + spawnInfo?.Enemy?.gameObject.name + " - " + positionString +
                                    " position.");
                }
            }
        }
    }
}