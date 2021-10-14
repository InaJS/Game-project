using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputListener : MonoBehaviour
{
    [SerializeField] private string keyName;
    [SerializeField] private UnityEvent callback;

    void Update()
    {
        bool keyPressed = Input.GetButtonDown(keyName);

        if (keyPressed)
        {
            callback.Invoke();
        }
    }
}
