using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxGenerator : MonoBehaviour
{
    [SerializeField] private int pointCount;
    
    private Vector3 baseDir = Vector3.right;

    public void CreateInDirection(Vector3 newDir)
    {
        baseDir = newDir;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position,this.transform.position + baseDir);
        
        
    }
}
