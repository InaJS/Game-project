using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class DanceInput : MonoBehaviour
{
    [SerializeField] private string danceButtonName;
    [SerializeField] private BpmValue currentSongBpm;
    [SerializeField] private FloatValue inputErrorMargin;
    [SerializeField] private FloatValue disableTime;
    [SerializeField] private Text debugTimerText;
    [SerializeField] private UnityEvent onCorrectInput;
    [SerializeField] private UnityEvent onWrongInput;
    [SerializeField] private UnityEvent onNoInput;
    
    private float timerInternal;
    private float blockedTime;
    private bool dancedOnTime;
    private bool dancedOutOfTime;
    
    void Update()
    {
        // 1. raise both timers and update the debug.text
        
        timerInternal += Time.deltaTime;
        debugTimerText.text = timerInternal.ToString();

        if (blockedTime > 0)
        {
            blockedTime -= Time.deltaTime;
            return;
        }

        // 2. then try to reset the timer if it's over the tempo
        
        bool passedInputWindow = timerInternal > currentSongBpm.value;

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
        
        bool withinInputWindow = timerInternal >= currentSongBpm.value - inputErrorMargin.value &&
                                 timerInternal <= currentSongBpm.value;

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