using System.Collections;
using System.Collections.Generic;
using CustomEventSystem;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/BasicValues/Float")]
public class FloatValue : ScriptableObject
{
    public float value;
    public UnityEvent callback;

    public void SetValue(float passedValue)
    {
        value = passedValue;
        callback.Invoke();
    }
}
