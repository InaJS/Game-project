using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCubeBump : MonoBehaviour
{
    private Transform _startingPosition;

    private void Awake()
    {
        _startingPosition = this.transform;
    }


    public void CubeBumpMethod()
    {
        this.transform.position = _startingPosition.position + Vector3.up*0.1f;
    }
}
