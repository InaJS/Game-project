using System.Collections;
using System.Collections.Generic;
using MiscUtil.Collections.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DanceTimer : MonoBehaviour
{
    [SerializeField] private string _danceButtonName;
    [Range(0.5f, 2.0f)] [SerializeField] private float _timeBetweenBeats;
    [Range(0.05f, 0.2f)] [SerializeField] private float _inputErrorMargin;
    [Range(0.1f, 1.0f)] [SerializeField] private float _disableTime;
    [SerializeField] private Text _debugTimerText;
    [SerializeField] private UnityEvent _onCorrectInput;
    [SerializeField] private UnityEvent _onWrongInput;
    [SerializeField] private UnityEvent _onNoInput;
    private float _timerInternal;
    private float _blockedTime;
    private bool _dancedOnTime;
    private bool _dancedOutOfTime;

    void Update()
    {
        // 1. raise both timers and update the debug.text
        
        _timerInternal += Time.deltaTime;
        _debugTimerText.text = _timerInternal.ToString();

        if (_blockedTime > 0)
        {
            _blockedTime -= Time.deltaTime;
            return;
        }

        // 2. then try to reset the timer if it's over the tempo
        
        bool passedInputWindow = _timerInternal > _timeBetweenBeats;

        if (passedInputWindow)
        {
            if (!_dancedOnTime && !_dancedOutOfTime) // if you didnt dance this beat, you dont get a debuff
            {
                _onNoInput.Invoke();
            }
            
            _timerInternal = 0;
            
            _dancedOnTime = false;
            _dancedOutOfTime = false;
            return;
        }
        
        // 3. lastly, if you're under the tempo, try to dance!
        
        bool withinInputWindow = _timerInternal >= _timeBetweenBeats - _inputErrorMargin &&
                                 _timerInternal <= _timeBetweenBeats;

        if (Input.GetButtonDown(_danceButtonName))
        {
            if (withinInputWindow)
            {
                _onCorrectInput.Invoke();
                _dancedOnTime = true;
            }
            else
            {
                _onWrongInput.Invoke();
                _blockedTime = _disableTime; // blocks the input for N seconds
                _dancedOutOfTime = true;
            }
        }
    }
}