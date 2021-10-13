using System;
using UnityEngine;
using System.Collections.Generic;


public class GoTowardsPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float minDistance = 0.5f;
    [SerializeField] private Animator myAnim;
    [SerializeField] private PushableObject pushable;
    private Transform target;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        myAnim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if ((target.position - transform.position).magnitude < minDistance)
        {
            return;
        }

        if (pushable.isBeingPushed)
        {
            return;
        }

        float stepDistance = Time.deltaTime * moveSpeed;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, stepDistance);
        
        myAnim.SetFloat("XSpeed", (target.position - transform.position).normalized.x);
        myAnim.SetFloat("YSpeed", (target.position - transform.position).normalized.y);
    }
}