using System;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class VolumeObserver : MonoBehaviour
{
    [SerializeField] private FloatValue volume;
    [SerializeField] private AudioSource audio;
    private float baseVolume;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        baseVolume = audio.volume;
        volume.callback.AddListener(AdjustAudioVolume);
        volume.SetValue(volume.value);
    }

    private void AdjustAudioVolume()
    {
        audio.volume = volume.value * baseVolume;
    }
}