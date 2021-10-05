using System;
using System.Collections;
using System.Collections.Generic;
using MiscUtil.Collections;
using UnityEngine;


[RequireComponent(typeof(PolygonCollider2D),typeof(PlayerMovement))]
public class HitboxGenerator : MonoBehaviour
{
    [Range(0.05f, 0.5f)] [SerializeField] private float duration;
    [SerializeField] private bool enableGizmo;

    [Header("Front points")] [Range(1, 10)] [SerializeField]
    private int forwardPointCount;

    [Range(5, 180)] [SerializeField] private float forwardAngleSpread;
    [Range(0.1f, 10.0f)] [SerializeField] private float forwardRadius;

    [Header("Back points")] [Range(0, 5)] [SerializeField]
    private int backPointCount;

    [Range(5, 180)] [SerializeField] private float backAngleSpread;
    [Range(0.1f, 10.0f)] [SerializeField] private float backRadius;

    [SerializeField] private Vector3 debugDir = Vector3.right;
    private PolygonCollider2D polyCollider;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        polyCollider = GetComponent<PolygonCollider2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TriggerDebugHitbox()
    {
        TriggerHitbox(debugDir);
    }

    public void TriggerHitboxOnPlayerDirection()
    {
        TriggerHitbox(playerMovement.PlayerInput);
    }

    public void TriggerHitbox(Vector3 dir)
    {
        GenerateColliderPointsToDirection(dir);
        StartCoroutine(EnableColliderTemporarily());
    }

    private IEnumerator EnableColliderTemporarily()
    {
        polyCollider.enabled = true;
        yield return new WaitForSeconds(duration);
        polyCollider.enabled = false;
    }

    private void GenerateColliderPointsToDirection(Vector3 hitboxDir)
    {
        hitboxDir.Normalize();
        List<Vector2> points = new List<Vector2>();

        backRadius = Mathf.Min(backRadius, forwardRadius);

        float backStartingAngle = -backAngleSpread / 2.0f;
        float angleStep = backAngleSpread / (backPointCount + 1);

        Quaternion firstRot = Quaternion.AngleAxis(backStartingAngle, Vector3.forward);

        Vector3 firstOffset = firstRot * hitboxDir * backRadius;
        Vector3 firstPoint = -firstOffset;
        points.Add(firstPoint);

        for (int i = 0; i <= backPointCount + 1; i++)
        {
            Quaternion rotate = Quaternion.AngleAxis(backStartingAngle + i * angleStep, Vector3.forward);
            Vector3 rotatedOffset = rotate * hitboxDir * backRadius;

            Vector3 newPoint = -rotatedOffset;
            points.Add(newPoint);
        }

        float forwardStartingAngle = -forwardAngleSpread / 2.0f;
        angleStep = forwardAngleSpread / (forwardPointCount);

        for (int i = 0; i <= forwardPointCount; i++)
        {
            Quaternion rotate = Quaternion.AngleAxis(forwardStartingAngle + i * angleStep, Vector3.forward);
            Vector3 rotatedOffset = rotate * hitboxDir * forwardRadius;

            Vector3 newPoint = rotatedOffset;
            points.Add(newPoint);
        }

        polyCollider.points = new Vector2[points.Count];
        polyCollider.points = points.ToArray();
    }

    // Gizmos ----------------------------------------------------------------------------------------------------

    private void OnDrawGizmos()
    {
        GenerateGizmoFan(debugDir);
    }

    private void GenerateGizmoFan(Vector3 direction)
    {
        if (!enableGizmo)
        {
            return;
        }

        Gizmos.color = Color.red;
        direction.Normalize();

        backRadius = Mathf.Min(backRadius, forwardRadius);

        float backStartingAngle = -backAngleSpread / 2.0f;
        float angleStep = backAngleSpread / (backPointCount + 1);

        Quaternion firstRot = Quaternion.AngleAxis(backStartingAngle, Vector3.forward);

        Vector3 firstOffset = firstRot * direction * backRadius;

        Vector3 firstPoint = transform.position - firstOffset;

        Vector3 prevPos = firstPoint;
        Vector3 currPos = firstPoint;

        for (int i = 0; i <= backPointCount + 1; i++)
        {
            Quaternion rotate = Quaternion.AngleAxis(backStartingAngle + i * angleStep, Vector3.forward);

            Vector3 rotatedOffset = rotate * direction * backRadius;

            currPos = transform.position - rotatedOffset;

            Gizmos.DrawLine(prevPos, currPos);

            prevPos = currPos;
        }

        float forwardStartingAngle = -forwardAngleSpread / 2.0f;
        angleStep = forwardAngleSpread / (forwardPointCount);

        for (int i = 0; i <= forwardPointCount; i++)
        {
            Quaternion rotate = Quaternion.AngleAxis(forwardStartingAngle + i * angleStep, Vector3.forward);

            Vector3 rotatedOffset = rotate * direction * forwardRadius;

            currPos = transform.position + rotatedOffset;

            Gizmos.DrawLine(prevPos, currPos);

            prevPos = currPos;
        }

        Gizmos.DrawLine(prevPos, firstPoint);

        Gizmos.color = Color.white;
    }
}