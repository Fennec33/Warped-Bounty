using System;
using UnityEngine;

namespace WarpedBounty.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float jumpForce = 100f;
        [SerializeField] private Transform groundCheckPoint;
        
        [SerializeField] private Animator animator;
        private readonly int a_IsMoving = Animator.StringToHash("IsMoving");
        private readonly int a_IsFacingUp = Animator.StringToHash("IsFacingUp");
        private readonly int a_IsDucking = Animator.StringToHash("IsDucking");
        private readonly int a_IsJumping = Animator.StringToHash("IsJumping");
        private readonly int a_StartJump = Animator.StringToHash("StartJump");
        
        private Rigidbody2D _rigidbody2D;
        private Vector3 _direction;

        private const float GroundCheckRadius = 0.01f;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector3 direction)
        {
            if (direction == _direction) return;
            _direction = direction;
            animator.SetBool(a_IsMoving, false);
            
            if (direction == Vector3.zero) return;
            FlipDirectionTo(direction.x);
            animator.SetBool(a_IsMoving, true);
        }

        public void Jump()
        {
            Collider2D[] colliders = new Collider2D[2];
            Physics2D.OverlapCircleNonAlloc(groundCheckPoint.position, GroundCheckRadius, colliders);
            foreach (var collision in colliders)
            {
                if (collision.gameObject == gameObject) continue;
                _rigidbody2D.AddForce(transform.up * jumpForce);
                animator.SetBool(a_IsJumping, true);
                animator.SetTrigger(a_StartJump);
                break;
            }
        }

        private void FlipDirectionTo(float dir)
        {
            var transform1 = transform;
            Vector3 newScale = transform1.localScale;
            newScale.x = dir;
            transform1.localScale = newScale;
        }

        private void FixedUpdate()
        {
            transform.position += Time.deltaTime * speed * _direction;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            animator.SetBool(a_IsJumping, false);
        }
    }
}
