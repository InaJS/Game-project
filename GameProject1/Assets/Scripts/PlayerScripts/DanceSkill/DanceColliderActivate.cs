using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DanceColliderActivate : MonoBehaviour
{
    private Collider2D collider;
    public float enableDuration;
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }
    public void TriggerHitbox()
    {
        StartCoroutine(EnableColliderTemporarily());
    }

    private IEnumerator EnableColliderTemporarily()
    {
        collider.enabled = true;
        yield return new WaitForSeconds(enableDuration);
        collider.enabled = false;
    }
}
