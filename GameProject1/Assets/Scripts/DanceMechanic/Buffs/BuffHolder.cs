using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuffHolder : MonoBehaviour
{
    [SerializeField] private Transform holder;
    [SerializeField] private FloatValue maxBuffs;
    [SerializeField] private SpriteRenderer buffPrefab;

    private void Awake()
    {
        if (holder == null)
        {
            holder = this.transform;
        }
    }

    public void RemoveBuffs()
    {
        foreach (Transform transform in holder)
        {
            Destroy(transform.gameObject);
        }
    }

    public void AddBuff()
    {
        if (holder.childCount >= maxBuffs.value)
        {
            return;
        }

        float randomizer = Random.Range(0.5f, 1.0f);

        Instantiate(buffPrefab, transform.position + randomizer * Vector3.right ,Quaternion.identity,this.transform);
    }
}
