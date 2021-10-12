using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    [SerializeField] private GameObject VFXPrefab;
    [SerializeField] private float particlesLifetime = 1.5f;

    public void SpawnVFX()
    {
        GameObject particles = Instantiate(VFXPrefab, this.transform.position, Quaternion.identity, null);
        
        Destroy(particles, particlesLifetime);
    }
}
