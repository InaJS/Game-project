using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuffHolder : MonoBehaviour
{
    [SerializeField] private Transform holder;
    [SerializeField] private FloatValue maxBuffs;
    [SerializeField] private FloatValue currentBuffs;
    [SerializeField] private SpriteRenderer buffPrefab;
    public float buffStacksBefore;

    private void Awake()
    {
        if (holder == null)
        {
            holder = this.transform;
        }

        buffStacksBefore = 0;
    }

    public void RefreshBuffs()
    {
        if (Mathf.Approximately(buffStacksBefore, maxBuffs.value) && 
            Mathf.Approximately(currentBuffs.value,maxBuffs.value))
        {
            return;
        }

        if (buffStacksBefore < currentBuffs.value)
        {
            AddBuff();
            return;
        }
        
        RemoveBuffs();
    }

    private void RemoveBuffs()
    {
        buffStacksBefore = 0;
        
        foreach (Transform transform in holder)
        {
            Destroy(transform.gameObject);
        }
    }

    private void AddBuff()
    {
        buffStacksBefore++;
        float radius = Random.Range(1.5f, 2.0f);
        float rotation = Random.Range(0.0f, 1.0f);

        Quaternion rotationQuat = Quaternion.AngleAxis(rotation * 360.0f, Vector3.forward);

        Instantiate(buffPrefab, transform.position + rotationQuat * Vector3.right * radius, Quaternion.identity,
            this.transform);
    }
}