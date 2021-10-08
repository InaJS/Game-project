using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TimeScaleController")]
public class TimeScaleController : ScriptableObject
{
    public void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }
}
