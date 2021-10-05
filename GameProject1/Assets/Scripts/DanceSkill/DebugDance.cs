using UnityEngine;
using UnityEngine.Events;

public class DebugDance : MonoBehaviour
{
    public string keyName;
    public UnityEvent callback;
    private Vector3 startingPosition;
    private bool dancing = true;

    private void Awake()
    {
        startingPosition = this.transform.position;
    }

    public void SuccesfulDance()
    {
        dancing = true;
    }

    public void TripOver()
    {
        dancing = false;
    }

    private void Update()
    {
        if (dancing)
        {
            transform.position = startingPosition + Vector3.right * Mathf.Sin(Time.time);
        }

        if (Input.GetKeyDown(keyName))
        {
            callback.Invoke();
        }
    }
}