using System;
using UnityEngine;
using System.Collections.Generic;


public class GoTowardsPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float animationDistance = 0.5f;
    [SerializeField] private float moveDistance = 0.5f;
    [SerializeField] private Animator myAnim;
    [SerializeField] private PushableObject pushable;
    private Transform target;

    private void Awake()
    {
        target = PlayerHealth.Instance.gameObject.transform;
        myAnim = GetComponentInChildren<Animator>();
        pushable = GetComponent<PushableObject>();
    }

    void Update()
    {
        float distanceToPlayer = (target.position - transform.position).magnitude;
        if (distanceToPlayer < animationDistance)
        {
            myAnim.SetBool("ShouldAttack",true);

            if (distanceToPlayer < moveDistance)
            {
                return;
            }
        }

        if (pushable.isBeingPushed)
        {
            return;
        }

        float stepDistance = Time.deltaTime * moveSpeed;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, stepDistance);

        myAnim.SetFloat("XSpeed", (target.position - transform.position).normalized.x);
        myAnim.SetFloat("YSpeed", (target.position - transform.position).normalized.y);
        myAnim.SetBool("ShouldAttack",false);
    }
}