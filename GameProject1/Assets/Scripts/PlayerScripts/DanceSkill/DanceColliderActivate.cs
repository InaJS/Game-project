using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DanceColliderActivate : MonoBehaviour
{
    private Collider2D collider;
    private SpriteRenderer renderer;
    public float enableDuration;
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }
    public void TriggerHitbox()
    {
        StartCoroutine(EnableColliderTemporarily());
    }

    private IEnumerator EnableColliderTemporarily()
    {
        collider.enabled = true;
        renderer.enabled = true;
        yield return new WaitForSeconds(enableDuration);
        collider.enabled = false;
        renderer.enabled = false;
    }
}
