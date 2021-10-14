using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HeartVisuals : MonoBehaviour
{
    [SerializeField] private FloatValue _currentHeartBuffs;
    [SerializeField] private FloatValue _comboNumber;
    [SerializeField] private SpawnZone[] spawnZones;
    [SerializeField] private DropParticles _heartVisual;
    [SerializeField] private Transform _holder;
    [SerializeField] private int _heartsBefore;
    [SerializeField] private bool enableGizmo;

    private List<DropParticles> hearts = new List<DropParticles>();

    public void RefreshHearts()
    {
        if (_heartsBefore <= _comboNumber.value && 
            Mathf.Approximately(_currentHeartBuffs.value,_comboNumber.value))
        {
            return;
        }

        if (_heartsBefore < _currentHeartBuffs.value)
        {
            AddHearts();
            return;
        }

        if (_heartsBefore > _currentHeartBuffs.value)
        {
            RemoveHearts();
        }
    }

    private void RemoveHearts()
    {
        for (var index = hearts.Count - 1; index >= 0; index--)
        {
            DropParticles dropper = hearts[index];
            dropper.LoseHearts();
        }

        _heartsBefore = 0;
        hearts.Clear();
    }

    public void UseHearts()
    {
        for (var index = hearts.Count - 1; index >= 0; index--)
        {
            DropParticles dropper = hearts[index];
            dropper.ConsumeHearts();
        }

        _heartsBefore = 0;
        hearts.Clear();
    }

    private void AddHearts()
    {
        _heartsBefore++;

        int index = Random.Range(0, spawnZones.Length);

        float horizontalRange = Random.Range(-1, 1)*0.5f;
        float verticalRange = Random.Range(-1, 1)*0.5f;

        Vector3 center = spawnZones[index].Center;

        Vector3 position = center + new Vector3(horizontalRange * spawnZones[index].Width, verticalRange * spawnZones[index].Height);

        DropParticles newHeart = Instantiate(_heartVisual, position, Quaternion.identity, _holder);

        newHeart.floating.startPosition = position;
        
        hearts.Add(newHeart);
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
