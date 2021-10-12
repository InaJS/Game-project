using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class DanceInput : MonoBehaviour
{
    [Header("Audio references ---------------------------------------")]
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip[] songs;

    [Header("Input ---------------------------------------")]
    [SerializeField] private string danceButtonName;
    [SerializeField] private float songTimeOffset = 0.1f;
    [SerializeField] private UnityEvent onCorrectInput;
    [SerializeField] private UnityEvent onWrongInput;
    [SerializeField] private UnityEvent onNoInput;

    [Header("Shader variables ---------------------------------------")]
    [SerializeField] private BpmValue currentSongBpm;
    [SerializeField] private FloatValue inputErrorMargin;
    [SerializeField] private FloatValue disableTime;
    [SerializeField] private FloatValue durationBuff;
    [SerializeField] private FloatValue distanceBuff;
    [SerializeField] private int maxBuffStacks = 0;
    [SerializeField] private float durationIncrement = 0;
    [SerializeField] private float distanceIncrement = 0;
    [SerializeField] private Material danceFloorSharedMaterial;

    private float timerInternal;
    private float timerInternalprevious;
    private float blockedTime;
    private bool dancedOnTime = false;
    private bool dancedOutOfTime = false;
    private float songPosition;
    private int currentSong;
    private float audioStartTime;
    private float lastDanced;
    private int buffStacks = 0;

    private void Awake()
    {
        if (audio == null)
        {
            audio = GetComponent<AudioSource>();
        }
        
        onCorrectInput.AddListener(BuffUp);
        onWrongInput.AddListener(ResetBuffs);
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

    public void BuffUp()
    {
        buffStacks++;
        buffStacks = Mathf.Clamp(buffStacks, 0, maxBuffStacks);

        durationBuff.value = durationIncrement * buffStacks;
        distanceBuff.value = distanceIncrement * buffStacks;
    }
    
    public void ResetBuffs()
    {
        buffStacks = 0;
    }

    void Update()
    {
        songPosition = (float) (AudioSettings.dspTime - audioStartTime); //  - songTimeOffset;
        
        danceFloorSharedMaterial.SetFloat("_SongTime", songPosition);
        // 1. raise both timers and update the debug.text
        
        timerInternal = songPosition % currentSongBpm.secsValue;

        if (blockedTime > 0)
        {
            blockedTime -= Time.deltaTime;
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
                lastDanced = songPosition;
                Debug.Log("danced on time");
            }
            else
            {
                onWrongInput.Invoke();
                blockedTime = disableTime.value; // blocks the input for N seconds on player mistake
                dancedOutOfTime = true;
                lastDanced = songPosition;
                Debug.Log("danced out of time");
            }
            return;
        }
        
        if (songPosition - lastDanced >= currentSongBpm.secsValue)
        {
            onNoInput.Invoke();
            dancedOnTime = false;
            dancedOutOfTime = false;
        }
    }
}