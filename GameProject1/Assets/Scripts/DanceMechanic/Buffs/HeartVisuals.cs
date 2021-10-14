using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartVisuals : MonoBehaviour
{
    [SerializeField] private FloatValue _currentHeartBuffs;
    [SerializeField] private FloatValue _comboNumber;
    [SerializeField] private SpawnZone[] spawnZones;
    [SerializeField] private SpriteRenderer _heartVisual;
    [SerializeField] private Transform _holder;
    [SerializeField] private int _heartsBefore;

    public void RefreshHearts()
    {
        if (Mathf.Approximately(_heartsBefore, _comboNumber.value) && 
            Mathf.Approximately(_currentHeartBuffs.value,_comboNumber.value))
        {
            return;
        }

        if (_heartsBefore < _currentHeartBuffs.value)
        {
            AddHearts();
            return;
        }
        
        RemoveHearts();
    }

    public void RemoveHearts()
    {
        _heartsBefore = 0;
        
        foreach (Transform transform in _holder)
        {
            Destroy(transform.gameObject);
        }
    }

    public void AddHearts()
    {
        _heartsBefore++;
        int index = Random.Range(0, spawnZones.Length);

        float horizontalRange = Random.Range(-1, 1)*0.5f;
        float verticalRange = Random.Range(-1, 1)*0.5f;

        Vector3 center = spawnZones[index].Center;

        Vector3 position = center + new Vector3(horizontalRange * spawnZones[index].Width, verticalRange * spawnZones[index].Height);

        Instantiate(_heartVisual, position, Quaternion.identity, _holder);
    }
}
