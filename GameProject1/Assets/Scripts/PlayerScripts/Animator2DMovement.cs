using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Animator))]
    public class Animator2DMovement : MonoBehaviour
    {
        [SerializeField] private string animatorHorizontalAxis;
        [SerializeField] private string animatorVerticalAxis;
        private Animator animator;

        public void SetAnimatorFloats(Vector2 direction)
        {
            animator.SetFloat(animatorHorizontalAxis, direction.x);
            animator.SetFloat(animatorVerticalAxis, direction.y);
        }
    }
}