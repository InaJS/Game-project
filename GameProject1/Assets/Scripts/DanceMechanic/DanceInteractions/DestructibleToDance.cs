using UnityEngine;


public class DestructibleToDance : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Dance"))
        {
            return;
        }

        BreakObject();
    }

    private void BreakObject()
    {
        Destroy(this.gameObject);
    }
}