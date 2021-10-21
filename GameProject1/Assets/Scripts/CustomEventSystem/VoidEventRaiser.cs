using CustomEventSystem;
using UnityEngine;


public class VoidEventRaiser : MonoBehaviour
{
    [SerializeField] private VoidEvent eventToRaise;

    public void RaiseEvent()
    {
        eventToRaise.Raise();
    }
}