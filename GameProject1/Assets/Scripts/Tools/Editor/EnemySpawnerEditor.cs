using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects, CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    public List<EnemyWave> EnemyWaveList;

    private void OnEnable()
    {
        EnemySpawner spawner = (EnemySpawner) target;
        EnemyWaveList = spawner.EnemyWaveList;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        for (int i = 0; i < EnemyWaveList.Count; i++)
        {
            EnemyWave wave = EnemyWaveList[i];
            EditorGUILayout.LabelField(wave.name);

            for (int j = 0; j < wave.Enemies.Count; j++)
            {
                SpawnInfo spawnInfo = wave.Enemies[j];
                
                if(spawnInfo == null || spawnInfo.Enemy == null)
                    continue;

                var positionString = "";

                positionString = spawnInfo.Position switch
                {
                    1 => "left",
                    2 => "down",
                    3 => "right",
                    _ => positionString
                };

                GUILayout.Label("   Enemy " + j + ": " + spawnInfo?.Enemy?.gameObject.name + " - " + positionString +
                                " position.");
            }
        }
    }
}