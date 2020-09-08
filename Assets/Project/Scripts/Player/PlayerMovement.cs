using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
        [SerializeField] private int numberOfAirJumps = 0;
        [SerializeField] private float clingingGravity = 0.15f;
        
        
        private Timer _jumpTimer;
        private int _jumpsExecuted = 0;
        private Rigidbody2D _rigidbody2D;
        private bool _jumpStopped = false;
        private bool _jumpedWithArrowKeys = false;
        private float _normalGravity;

        public void Move(Vector2 direction)
        {
            _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
            
            if (_jumpedWithArrowKeys && direction == Vector2.zero)
            {
                StopJump();
            }

            if (player.IsClinging() && player.Direction == direction)
            {
                player.IsMoving(true);
                Jump();
            }
            else if (!player.IsClinging())
            {
                SetDirection(direction);
                if (direction != Vector2.zero)
                    player.IsMoving(true);
                else
                    player.IsMoving(false);
            }
        }
        
        public void Jump()
        {
            _jumpStopped = false;
            
            if (player.IsClinging())
            {
                player.IsClinging(false);
                _jumpedWithArrowKeys = true;
                
                if (!player.IsMoving())
                {
                    _jumpedWithArrowKeys = false;
                    _rigidbody2D.velocity = new Vector2(jumpForce / 2 * player.Direction.x, _rigidbody2D.velocity.y);
                }
            }

            if (player.TimeSinceLastGrounded <= coyoteTime || _jumpsExecuted <= numberOfAirJumps)
            {
                _jumpsExecuted++;
                _jumpTimer.ResetTime();
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
                player.IsJumping(true);
                player.StartJumpAnimation();
            }
        }
        
        public void StopJump()
        {
            _jumpStopped = true;
            _jumpedWithArrowKeys = false;
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

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _jumpTimer = gameObject.AddComponent<Timer>();
            _normalGravity = _rigidbody2D.gravityScale;
        }

        private void SetDirection(Vector2 direction)
        {
            if (direction == player.Direction) return;
            if (direction == Vector2.zero) return;
            
            player.Direction = direction;

            var t = transform;
            var v = t.localScale;
            v.x = direction.x;
            t.localScale = v;
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
                var directionV = new Vector3(direction * -1f, 0f, 0f);
                SetDirection(directionV);
                _rigidbody2D.gravityScale = clingingGravity;
                player.IsMoving(false);
                player.IsClinging(true);
                _jumpsExecuted = 0;
            }
        }
        
        private void FixedUpdate()
        {
            if(player.IsMoving() && !player.IsClinging())
                transform.position += Time.deltaTime * speed * (Vector3) player.Direction;
            
            if (_jumpStopped && _rigidbody2D.velocity.y > 0 && !player.IsClinging())
            {
                var velocity = _rigidbody2D.velocity;
                velocity = new Vector2(velocity.x, velocity.y * 0.8f);
                _rigidbody2D.velocity = velocity;
            }
        }

        private void Update()
        {
            if (!player.IsOnWall() || player.IsGrounded())
            {
                _rigidbody2D.gravityScale = _normalGravity;
                player.IsClinging(false);
            }
        }
    }
}
