using UnityEngine;
using UnityEngine.Events;


public class TimerEventSingle : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5;
    [SerializeField] private UnityEvent callback;
    
    public delegate void OnTimerDone();
    public OnTimerDone timerCallback;

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            callback.Invoke();
            timerCallback.Invoke();
        }
    }
}