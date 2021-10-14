using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BasicValues/BpmValue")]
public class BpmValue : ScriptableObject
{
    [SerializeField] private int bpm = 1;
    public float secsValue;

    public int BPM
    {
        get
        {
            return bpm;
        }
        
        set
        {
            bpm = value;
            secsValue = 60.0f / bpm;
        }
    }
    
    private void OnValidate()
    {
        bpm = Mathf.Clamp(bpm, 1, bpm);
        secsValue = 60.0f / bpm;
    }
}