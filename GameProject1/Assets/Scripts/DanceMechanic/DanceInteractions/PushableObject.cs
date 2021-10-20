using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PushableObject : MonoBehaviour
{
    [SerializeField] private float baseBouncyness;
    [SerializeField] private float pushBuffMultiplier = 1;
    [SerializeField] private FloatValue pushBuffDistance;
    [SerializeField] private float baseStunTime;
    [SerializeField] private float stunBuffMultiplier = 1;
    [SerializeField] private FloatValue stunBuffDuration;
    
    [HideInInspector] public bool isBeingPushed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Dance"))
        {
           return; 
        }
        
        Vector3 pushDirection = this.transform.position - other.transform.position;

        float pushValue  = Mathf.Clamp(baseBouncyness + pushBuffDistance.value, 0, baseBouncyness + pushBuffDistance.value);
        float pushtime = Mathf.Clamp(baseStunTime + stunBuffDuration.value, 0, baseStunTime + stunBuffDuration.value);

        if (pushValue <= 0)
        {
            pushtime = 0;
        }

        TryPush(pushDirection, pushBuffMultiplier* pushValue,stunBuffMultiplier * pushtime);
    }

    public void TryPush(Vector3 dir, float force, float duration)
    {
        if (isBeingPushed)
        {
            return;
        }

        StartCoroutine(Push(dir.normalized, force, duration));
    }

    private IEnumerator Push(Vector3 dir, float force, float duration)
    {
        isBeingPushed = true;
        
        while (duration > 0)
        {
            transform.position += dir * force * Time.deltaTime;
            yield return null;
            duration -= Time.deltaTime;
        }

        isBeingPushed = false;
    }
}