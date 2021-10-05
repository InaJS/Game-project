using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class DanceInput : MonoBehaviour
{
    [SerializeField] private string danceButtonName;
    [Range(0.5f, 2.0f)] [SerializeField] private float timeBetweenBeats;
    [Range(0.05f, 0.2f)] [SerializeField] private float inputErrorMargin;
    [Range(0.1f, 1.0f)] [SerializeField] private float disableTime;
    [SerializeField] private Text debugTimerText;
    [SerializeField] private UnityEvent onCorrectInput;
    [SerializeField] private UnityEvent onWrongInput;
    [SerializeField] private UnityEvent onNoInput;
    
    private PlayerMovement playerMovement;
    private HitboxGenerator hitboxGenerator;
    private float timerInternal;
    private float blockedTime;
    private bool dancedOnTime;
    private bool dancedOutOfTime;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

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
        
        bool passedInputWindow = timerInternal > timeBetweenBeats;

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
        
        bool withinInputWindow = timerInternal >= timeBetweenBeats - inputErrorMargin &&
                                 timerInternal <= timeBetweenBeats;

        if (Input.GetButtonDown(danceButtonName))
        {
            if (withinInputWindow)
            {
                onCorrectInput.Invoke();
                dancedOnTime = true;
            }
            else
            {
                onWrongInput.Invoke();
                blockedTime = disableTime; // blocks the input for N seconds
                dancedOutOfTime = true;
            }
        }
    }
}