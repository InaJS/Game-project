using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PushableObject : MonoBehaviour
{
    [SerializeField] private float bouncyness;
    [SerializeField] private FloatValue pushBuffDuration;
    [SerializeField] private FloatValue pushBuffDistance;
    [SerializeField] private float stunTime;
    private bool isBeingPushed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Dance"))
        {
           return; 
        }
        Vector3 pushDirection = this.transform.position - other.transform.position;
        TryPush(pushDirection,bouncyness + pushBuffDistance.value,stunTime + pushBuffDuration.value);
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