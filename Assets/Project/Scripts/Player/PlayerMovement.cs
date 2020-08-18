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
        [SerializeField] private float coyoteTime = 0.1f;
        [SerializeField] private Collider2D standingCollider;
        [SerializeField] private Collider2D duckingCollider;
        [SerializeField] [Range(0, 3)] private int numberOfAirJumps = 0;

        private float _timeSinceJumpExecuted = 0f;
        private int _jumpsExecuted = 0;
        private Rigidbody2D _rigidbody2D;

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
            if (player.TimeSinceLastGrounded <= coyoteTime || _jumpsExecuted <= numberOfAirJumps)
            {
                _jumpsExecuted++;
                _timeSinceJumpExecuted = 0f;
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
                player.IsJumping(true);
                player.StartJumpAnimation();
            }
        }

        public void StopJump()
        {
            if (_rigidbody2D.velocity.y > 0)
            {
                var velocity = _rigidbody2D.velocity;
                velocity = new Vector2(velocity.x, velocity.y / 2);
                _rigidbody2D.velocity = velocity;
            }
        }

        public void UpDown(float axis)
        {
            if (axis > 0.2f)
            {
                player.IsDucking(false);
                player.IsFacingUp(true);
                if (standingCollider.enabled == false)
                {
                    standingCollider.enabled = true;
                    duckingCollider.enabled = false;
                }
            }
            else if (axis < -0.2f)
            {
                player.IsFacingUp(false);
                player.IsDucking(true);
                if (standingCollider.enabled == true)
                {
                    duckingCollider.enabled = true;
                    standingCollider.enabled = false;
                }
            }
            else
            {
                player.IsFacingUp(false);
                player.IsDucking(false);
                if (standingCollider.enabled == false)
                {
                    standingCollider.enabled = true;
                    duckingCollider.enabled = false;
                }
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
            if (player.IsGrounded() && _timeSinceJumpExecuted >= 0.1f)
            {
                player.IsJumping(false);
                _jumpsExecuted = 0;
            }

            if (player.IsOnWall() && !player.IsGrounded())
            {
                var direction = transform.localScale.x;
                direction *= -1;
                FlipDirectionTo(direction);
                player.IsClinging(true);
            }
        }

        private void Update()
        {
            _timeSinceJumpExecuted += Time.deltaTime;
            
            if (!player.IsOnWall() || player.IsGrounded())
            {
                player.IsClinging(false);
            }
        }
    }
}
