using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCursorInput : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector2> simpleCursorEvent;
    [SerializeField] private UnityEvent<Vector2> directionalCursorEvent;
    private Vector3 cursorPos;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    void Update()
    {
        cursorPos = camera.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0;
        Vector2 relativePosition = cursorPos - this.transform.position;
        
        if (Input.GetMouseButton(0))
        {
            simpleCursorEvent?.Invoke(cursorPos);
            directionalCursorEvent?.Invoke(relativePosition);
        }
    }
}
