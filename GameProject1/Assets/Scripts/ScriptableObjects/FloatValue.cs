using System.Collections;
using System.Collections.Generic;
using CustomEventSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BasicValues/Float")]
public class FloatValue : ScriptableObject
{
    public float value;

    public void SetValue(float passedValue)
    {
        value = passedValue;
    }
}
