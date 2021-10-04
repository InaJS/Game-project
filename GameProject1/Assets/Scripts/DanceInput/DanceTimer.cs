using System.Collections;
using System.Collections.Generic;
using MiscUtil.Collections.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DanceTimer : MonoBehaviour
{
    [SerializeField] private string _danceButtonName;
    [Range(0.5f,2.0f)][SerializeField] private float _timeBetweenBeats;
    [Range(0.01f,0.1f)][SerializeField] private float _errorMargin;
    [SerializeField] private Text _text;
    [SerializeField] private UnityEvent _pressedDanceCallback;
    private float _timerInternal;

    void Update()
    {
        _timerInternal += Time.deltaTime;
        _text.text = _timerInternal.ToString();

        bool withinInputWindow = _timerInternal >= _timeBetweenBeats - _errorMargin && _timerInternal <= _timeBetweenBeats + _errorMargin;
        
        if (withinInputWindow)
        {
            if (Input.GetButtonDown(_danceButtonName))
            {
                _pressedDanceCallback.Invoke();
            }
        }

        bool passedInputWindow = _timerInternal > _timeBetweenBeats + _errorMargin;

        if (passedInputWindow)
        {
            _timerInternal = 0;
        }
    }
}
