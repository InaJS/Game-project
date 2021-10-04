using UnityEngine;

public class DebugCubeBump : MonoBehaviour
{
    private Vector3 _startingPosition;
    private bool _dancing = true;

    private void Awake()
    {
        _startingPosition = this.transform.position;
    }

    public void SuccesfulDance()
    {
        _dancing = true;
    }

    public void TripOver()
    {
        _dancing = false;
    }

    private void Update()
    {
        if (_dancing)
        {
            transform.position = _startingPosition + Vector3.right * Mathf.Sin(Time.time);
        }
    }
}
