using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PushableObject : MonoBehaviour
{
    [SerializeField] private float bouncyness;
    [SerializeField] private float stunTime;
    private bool isBeingPushed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 pushDirection = this.transform.position - other.transform.position;
        TryPush(pushDirection,bouncyness,stunTime);
    }

    public void TryPush(Vector3 dir, float force, float duration)
    {
        if (isBeingPushed)
        {
            return;
        }

        isBeingPushed = true;
        StartCoroutine(Push(dir.normalized, force, duration));
    }

    private IEnumerator Push(Vector3 dir, float force, float duration)
    {
        while (duration > 0)
        {
            transform.position += dir * force * Time.deltaTime;
            yield return null;
            duration -= Time.deltaTime;
        }

        isBeingPushed = false;
    }
}