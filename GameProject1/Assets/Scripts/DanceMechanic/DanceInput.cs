using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class DanceInput : MonoBehaviour
{
    [Header("Audio references ---------------------------------------")] [SerializeField]
    private AudioSource audio;

    [SerializeField] private SongWaves songSettings;

    [Header("Input timing ---------------------------------------")] [SerializeField]
    private string danceButtonName;

    [SerializeField] private FloatValue inputErrorMargin;
    [SerializeField] private FloatValue disableTime;
    [SerializeField] private float songTimeOffset = 0.1f;
    [SerializeField] private UnityEvent onCorrectInput;
    [SerializeField] private UnityEvent onWrongInput;
    [SerializeField] private UnityEvent onNoInput;

    [Header("Buff variables ---------------------------------------")] [SerializeField]
    private FloatValue buffStacks;

    [SerializeField] private FloatValue maxBuffStacks;
    [SerializeField] private FloatValue durationBuff;
    [SerializeField] private FloatValue distanceBuff;
    [SerializeField] private float durationIncrement = 0;
    [SerializeField] private float distanceIncrement = 0;
    [SerializeField] private FloatValue currentHealCounter;
    [SerializeField] private FloatValue healComboNumber;
    [SerializeField] private int healValue = 1;

    [Header("Shader Material ---------------------------------------")] [SerializeField]
    private Material danceFloorSharedMaterial;

    private float timerInternal;
    private float timerInternalprevious;
    private float blockedTime;
    private bool dancedOnTime = false;
    private bool dancedOutOfTime = false;
    private float songPosition;
    private int currentSong;
    private float audioStartTime;
    private float lastDanced;

    private void Awake()
    {
        if (audio == null)
        {
            audio = GetComponent<AudioSource>();
        }

        onCorrectInput.AddListener(BuffUp);
        onWrongInput.AddListener(ResetBuffs);
        onNoInput.AddListener(ResetBuffs);

        buffStacks.value = 0;
        currentHealCounter.value = 0;

        AdjustSong();
    }

    private void OnDisable()
    {
        onCorrectInput.RemoveAllListeners();
        onWrongInput.RemoveAllListeners();
        onNoInput.RemoveAllListeners();
    }

    void AdjustSong()
    {
        audio.Stop();
        audioStartTime = (float) AudioSettings.dspTime;

        audio.clip = songSettings.SongAudio;
        audio.Play();

        danceFloorSharedMaterial.SetFloat("_DelayBetweenFlashes", songSettings.SongBpm.secsValue);
        danceFloorSharedMaterial.SetFloat("_DelayBetweenFlashes", songSettings.SongBpm.secsValue);
        danceFloorSharedMaterial.SetFloat("_FlashDuration", inputErrorMargin.value);
    }

    public void BuffUp()
    {
        if (currentHealCounter.value >= healComboNumber.value)
        {
            currentHealCounter.value = 0;
        }
        
        buffStacks.value++;
        currentHealCounter.value++;

        buffStacks.value = (int) Mathf.Clamp(buffStacks.value, 0, maxBuffStacks.value);

        durationBuff.value = durationIncrement * buffStacks.value;
        distanceBuff.value = distanceIncrement * buffStacks.value;

        if (currentHealCounter.value >= healComboNumber.value)
        {
            PlayerHealth.Instance.HealPlayer(healValue);
        }
    }

    public void ResetBuffs()
    {
        buffStacks.value = 0;
        currentHealCounter.value = 0;
    }

    void Update()
    {
        if (Time.timeScale < 0.1f)
        {
            return;
        }

        songPosition = (float) (AudioSettings.dspTime - audioStartTime); //  - songTimeOffset;

        danceFloorSharedMaterial.SetFloat("_SongTime", songPosition);
        // 1. raise both timers and update the debug.text

        timerInternal = songPosition % songSettings.SongBpm.secsValue;

        if (blockedTime > 0)
        {
            blockedTime -= Time.deltaTime;
            return;
        }

        // 3. lastly, if you're under the tempo, try to dance!

        bool withinInputWindow =
            (timerInternal >= songSettings.SongBpm.secsValue - inputErrorMargin.value &&
             timerInternal <= songSettings.SongBpm.secsValue) || timerInternal < inputErrorMargin.value;

        if (Input.GetButtonDown(danceButtonName))
        {
            if (withinInputWindow)
            {
                onCorrectInput.Invoke();
                dancedOnTime = true;
                lastDanced = songPosition;
                // Debug.Log("danced on time");
            }
            else
            {
                onWrongInput.Invoke();
                blockedTime = disableTime.value; // blocks the input for N seconds on player mistake
                dancedOutOfTime = true;
                lastDanced = songPosition;
                // Debug.Log("danced out of time");
            }

            return;
        }

        if (songPosition - lastDanced >= songSettings.SongBpm.secsValue + inputErrorMargin.value)
        {
            onNoInput.Invoke();
            dancedOnTime = false;
            dancedOutOfTime = false;
            // Debug.Log("Missed the input window: current time" + songPosition + " last input time:" + lastDanced);
        }
    }
}