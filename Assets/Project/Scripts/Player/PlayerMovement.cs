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
            
            if (direction == Vector3.zero) return;
            FlipDirectionTo(direction.x);
        }

        public void Jump()
        {
            Collider2D[] colliders = new Collider2D[2];
            Physics2D.OverlapCircleNonAlloc(groundCheckPoint.position, GroundCheckRadius, colliders);
            foreach (var collision in colliders)
            {
                if (collision.gameObject == gameObject) continue;
                _rigidbody2D.AddForce(transform.up * jumpForce);
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
    }
}
