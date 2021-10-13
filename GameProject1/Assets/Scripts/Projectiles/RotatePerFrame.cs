using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePerFrame : MonoBehaviour
{
    [Range(0.5f,10.0f)][SerializeField] private float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward,Time.deltaTime * rotationSpeed * 360.0f);
    }
}
