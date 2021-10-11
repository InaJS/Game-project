using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class DanceInput : MonoBehaviour
{
    [SerializeField] private string danceButtonName;
    [SerializeField] private BpmValue currentSongBpm;
    [SerializeField] private FloatValue inputErrorMargin;
    [SerializeField] private FloatValue disableTime;
    [SerializeField] private UnityEvent onCorrectInput;
    [SerializeField] private UnityEvent onWrongInput;
    [SerializeField] private UnityEvent onNoInput;
    [SerializeField] private AudioSource audio;
    [SerializeField] private Material danceFloorSharedMaterial;
    [SerializeField] private AudioClip[] songs;
    
    private float timerInternal;
    private float blockedTime;
    private bool dancedOnTime;
    private bool dancedOutOfTime;
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
        danceFloorSharedMaterial.SetFloat("_FlashDuration", inputErrorMargin.value);
    }
    
    void Update()
    {
        songPosition = (float) (AudioSettings.dspTime - audioStartTime);
        
        danceFloorSharedMaterial.SetFloat("_SongTime", songPosition);
        // 1. raise both timers and update the debug.text
        
        timerInternal = songPosition % currentSongBpm.secsValue;

        if (blockedTime > 0)
        {
            blockedTime -= Time.deltaTime;
            return;
        }

        // 2. then try to reset the timer if it's over the tempo
        
        bool passedInputWindow = timerInternal > currentSongBpm.secsValue;

        if (passedInputWindow)
        {
            if (!dancedOnTime && !dancedOutOfTime) // if you didnt dance this beat, you dont get a debuff
            {
                onNoInput.Invoke();
            }

            timerInternal = 0;

            dancedOnTime = false;
            dancedOutOfTime = false;
            return;
        }
        
        // 3. lastly, if you're under the tempo, try to dance!
        
        bool withinInputWindow = (timerInternal >= currentSongBpm.secsValue - inputErrorMargin.value && timerInternal <= currentSongBpm.secsValue) || timerInternal < inputErrorMargin.value;

        if (Input.GetButtonDown(danceButtonName))
        {
            if (withinInputWindow)
            {
                onCorrectInput.Invoke();
                dancedOnTime = true;
                Debug.Log("danced on time");
            }
            else
            {
                onWrongInput.Invoke();
                blockedTime = disableTime.value; // blocks the input for N seconds
                dancedOutOfTime = true;
                Debug.Log("danced out of time");
            }
        }
    }
}