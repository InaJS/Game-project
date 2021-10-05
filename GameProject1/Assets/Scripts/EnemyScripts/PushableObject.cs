using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PushableObject : MonoBehaviour
{
    public void TryPush(Vector3 dir, float force, float duration)
    {
        StartCoroutine(Push(dir.normalized, force, duration));
    }

    private IEnumerator Push(Vector3 dir, float force, float duration)
    {
        while (duration > 0)
        {
            transform.position += dir * force * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            duration -= Time.deltaTime;
        }
    }
}
