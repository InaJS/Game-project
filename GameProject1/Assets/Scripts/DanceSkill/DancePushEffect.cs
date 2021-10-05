using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class DancePushEffect : MonoBehaviour
{
    private PolygonCollider2D polyCollider;

    private void Awake()
    {
        polyCollider = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
    
    
}
