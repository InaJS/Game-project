using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private string[] tagsToCollideWith;
    [SerializeField] private GameObject VFXPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string targetTag in tagsToCollideWith)
        {
            if (collision.gameObject.CompareTag(targetTag))
            {
                Destroy(this.gameObject, 0.01f);
                break;
            }
        }
    }
}