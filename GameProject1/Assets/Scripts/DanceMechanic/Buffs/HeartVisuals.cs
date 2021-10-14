using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartVisuals : MonoBehaviour
{
    [SerializeField] private FloatValue _currentHeartBuffs;
    [SerializeField] private FloatValue _comboNumber;
    [SerializeField] private SpriteRenderer _heartVisual;
    [SerializeField] private Transform _holder;
    [SerializeField] private int _heartsBefore;

    public void RefreshHearts()
    {
        if (Mathf.Approximately(_heartsBefore, _comboNumber.value) && 
            Mathf.Approximately(_currentHeartBuffs.value,_comboNumber.value))
        {
            return;
        }

        if (_heartsBefore < _currentHeartBuffs.value)
        {
            AddBuff();
            return;
        }
        
        RemoveBuffs();
    }

    public void RemoveBuffs()
    {
        _heartsBefore = 0;
        
        foreach (Transform transform in _holder)
        {
            Destroy(transform.gameObject);
        }
    }

    public void AddBuff()
    {
        _heartsBefore++;
        float radius = Random.Range(1.5f, 2.0f);
        float rotation = Random.Range(0.0f, 1.0f);

        Quaternion rotationQuat = Quaternion.AngleAxis(rotation * 360.0f, Vector3.forward);

        Instantiate(_heartVisual, transform.position + rotationQuat * Vector3.right * radius, Quaternion.identity,
            this.transform);
    }
}
