using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SongTimeShader : MonoBehaviour
{
    [SerializeField] private float songPosition;
    [SerializeField] private float songPosInBeats;
    [SerializeField] private BpmValue currentSongBpm;
    [SerializeField] private AudioSource audio;
    private float secPerBeat;
    private float dsptimesong;

    private void Awake()
    {
        if (audio == null)
        {
            audio = GetComponent<AudioSource>();
        }
        
    }

    void NewSong()
    {
        //calculate how many seconds is one beat
        //we will see the declaration of bpm later
        secPerBeat = currentSongBpm.secsValue;

        //record the time when the song starts
        dsptimesong = (float) AudioSettings.dspTime;

        //start the song
        audio.Play();
    }

    private void Update()
    {
        throw new NotImplementedException();
    }
}