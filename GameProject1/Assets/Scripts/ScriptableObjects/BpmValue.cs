using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BasicValues/BpmValue")]
public class BpmValue : ScriptableObject
{
    public int bpm = 1;
    public float value;

    private void OnValidate()
    {
        bpm = Mathf.Clamp(bpm, 1, bpm);
        value = 60.0f / bpm;
    }
}