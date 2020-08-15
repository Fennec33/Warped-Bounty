using System;
using UnityEngine;

namespace WarpedBounty.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerInfo player;
        [SerializeField] private float speed = 1f;
        [SerializeField] private float jumpForce = 100f;
        [SerializeField] private Transform groundCheckPoint;

        private Rigidbody2D _rigidbody2D;

        private const float GroundCheckRadius = 0.01f;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector3 direction)
        {
            if (direction == player.Direction) return;
            player.Direction = direction;
            player.IsMoving(false);
            
            if (direction == Vector3.zero) return;
            FlipDirectionTo(direction.x);
            player.IsMoving(true);
        }

        public void Jump()
        {
            Collider2D[] colliders = new Collider2D[2];
            Physics2D.OverlapCircleNonAlloc(groundCheckPoint.position, GroundCheckRadius, colliders);
            foreach (var collision in colliders)
            {
                if (collision.gameObject == gameObject) continue;
                _rigidbody2D.AddForce(transform.up * jumpForce);
                player.IsJumping(true);
                player.StartJumpAnimation();
                break;
            }
        }

        public void UpDown(float axis)
        {
            if (axis > 0f)
            {
                player.IsDucking(false);
                player.IsFacingUp(true);
            }
            else if (axis < 0f)
            {
                player.IsFacingUp(false);
                player.IsDucking(true);
            }
            else
            {
                player.IsFacingUp(false);
                player.IsDucking(false);
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
            transform.position += Time.deltaTime * speed * player.Direction;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            player.IsJumping(false);
        }
    }
}
