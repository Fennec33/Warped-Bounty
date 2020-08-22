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
        
        private Timer _jumpTimer;
        private int _jumpsExecuted = 0;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _jumpTimer = gameObject.AddComponent<Timer>();
        }

        public void SetDirection(Vector3 direction)
        {
            if (direction == player.Direction) return;
            if (direction == Vector3.zero)
            {
                player.Direction = direction;
            }
            else
            {
                player.Direction = direction;
                FlipDirectionTo(direction.x);
            }
        }

        public void LookUp()
        {
            player.IsDucking(false);
            player.IsFacingUp(true);
            if (!standingCollider.enabled)
            {
                standingCollider.enabled = true;
                duckingCollider.enabled = false;
            }
        }
        
        public void Duck()
        {
            player.IsFacingUp(false);
            player.IsDucking(true);
            if (standingCollider.enabled)
            {
                duckingCollider.enabled = true;
                standingCollider.enabled = false;
            }
        }
        
        public void Stand()
        {
            player.IsFacingUp(false);
            player.IsDucking(false);
            if (!standingCollider.enabled)
            {
                standingCollider.enabled = true;
                duckingCollider.enabled = false;
            }
        }
        
        public void StartMoving()
        {
            player.IsMoving(true);
        }
        
        public void StopMoving()
        {
            player.IsMoving(false);
        }
        
        public void Jump()
        {
            if (player.TimeSinceLastGrounded <= coyoteTime || _jumpsExecuted <= numberOfAirJumps)
            {
                _jumpsExecuted++;
                _jumpTimer.ResetTime();
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
                player.IsJumping(true);
                player.StartJumpAnimation();
            }
        }

        public void JumpOffWall()
        {
            if (player.TimeSinceLastGrounded <= coyoteTime || _jumpsExecuted <= numberOfAirJumps)
            {
                Debug.Log("jumping off wall");
                _jumpsExecuted++;
                _jumpTimer.ResetTime();
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

        private void FlipDirectionTo(float dir)
        {
            var transform1 = transform;
            Vector3 newScale = transform1.localScale;
            newScale.x = dir;
            transform1.localScale = newScale;
        }

        private void FixedUpdate()
        {
            if(player.IsMoving())
                transform.position += Time.deltaTime * speed * player.Direction;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (player.IsGrounded() && _jumpTimer.GetTime() >= 0.1f)
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
            if (!player.IsOnWall() || player.IsGrounded())
            {
                player.IsClinging(false);
            }
        }
    }
}
