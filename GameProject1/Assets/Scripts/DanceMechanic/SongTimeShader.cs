using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SongTimeShader : MonoBehaviour
{
    [SerializeField] private BpmValue currentSongBpm;
    [SerializeField] private FloatValue flashDuration;
    [SerializeField] private AudioSource audio;
    [SerializeField] private Material danceFloorSharedMaterial;
    [SerializeField] private AudioClip[] songs;
    private float songPosition;
    private int currentSong;
    private float audioStartTime;

    private void Awake()
    {
        if (audio == null)
        {
            audio = GetComponent<AudioSource>();
        }
    }

    void NewSong()
    {
        audio.Stop();
        audioStartTime = (float) AudioSettings.dspTime;

        audio.clip = songs[currentSong];
        audio.Play();
        
        danceFloorSharedMaterial.SetFloat("_DelayBetweenFlashes", currentSongBpm.secsValue);
        danceFloorSharedMaterial.SetFloat("_FlashDuration", flashDuration.value);
    }

    void Update()
    {
        songPosition = (float) (AudioSettings.dspTime - audioStartTime);
        
        danceFloorSharedMaterial.SetFloat("_SongTime", songPosition);
    }
}