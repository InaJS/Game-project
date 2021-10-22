using System;
using UnityEngine;


public class Animator2DMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animatorHorizontalAxis;
    [SerializeField] private string animatorVerticalAxis;
    [SerializeField] private string animatorShouldMove;

    public void SetAnimatorFloats(Vector2 direction)
    {
        if (direction.magnitude <= Single.Epsilon)
        {
            animator.SetBool(animatorShouldMove, false);
        }
        else
        {
            animator.SetBool(animatorShouldMove, true);
        }

        animator.SetFloat(animatorHorizontalAxis, direction.x);
        animator.SetFloat(animatorVerticalAxis, direction.y);
    }
}