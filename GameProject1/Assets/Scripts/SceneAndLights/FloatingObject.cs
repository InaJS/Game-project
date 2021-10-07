using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 floatDirection;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementDistance;
    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + floatDirection * movementDistance * Mathf.Sin(movementSpeed * Time.time);
    }
}
