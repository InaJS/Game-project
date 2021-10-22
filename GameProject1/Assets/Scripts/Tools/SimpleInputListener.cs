using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleInputListener : MonoBehaviour
{
    [SerializeField] private string buttonName;
    [SerializeField] private UnityEvent callback;

    void Update()
    {
        bool keyPressed = Input.GetButtonDown(buttonName);

        if (keyPressed)
        {
            callback.Invoke();
        }
    }
}
