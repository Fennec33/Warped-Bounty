using System;
using UnityEngine;

namespace WarpedBounty.Player
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private Transform groundCheckPoint1;
        [SerializeField] private Transform groundCheckPoint2;
        [SerializeField] private Transform wallCheckPoint1;
        [SerializeField] private Transform wallCheckPoint2;
        [SerializeField] private Animator animator;
        #region Animator Strings To Hash
        private readonly int _isMoving = Animator.StringToHash("IsMoving");
        private readonly int _isFacingUp = Animator.StringToHash("IsFacingUp");
        private readonly int _isDucking = Animator.StringToHash("IsDucking");
        private readonly int _isJumping = Animator.StringToHash("IsJumping");
        private readonly int _isClinging = Animator.StringToHash("IsClinging");
        private readonly int _startJump = Animator.StringToHash("StartJump");
        private readonly int _timeSinceLastShoot = Animator.StringToHash("TimeSinceLastShoot");
        private readonly int _hurt = Animator.StringToHash("Hurt");
        #endregion

        private LayerMask _walkableSurfaceMask;
        private LayerMask _clingableSurfaceMask;
        private const float GroundCheckRadius = 0.01f;
        private Vector2 _direction;
        public Vector2 Direction 
        {
            get => _direction;
            set
            {
                _direction = value.normalized;
                if (_direction != Vector2.zero)
                    DirectionFacing = _direction;
            }
        }
        public Vector3 DirectionFacing { get; private set; }
        public float TimeSinceLastGrounded { get; private set; }

        private void Start()
        {
            _walkableSurfaceMask = LayerMask.GetMask("Wall", "Platform");
            _clingableSurfaceMask = LayerMask.GetMask("Wall");
            DirectionFacing = Vector2.right;
            TimeSinceLastGrounded = 0f;
        }

        #region Animation Controll
        public bool IsMoving() => animator.GetBool(_isMoving);
        public void IsMoving(bool value) => animator.SetBool(_isMoving, value);
        public bool IsFacingUp() => animator.GetBool(_isFacingUp);
        public void IsFacingUp(bool value) => animator.SetBool(_isFacingUp, value);
        public bool IsDucking() => animator.GetBool(_isDucking);
        public void IsDucking(bool value) => animator.SetBool(_isDucking, value);
        public bool IsJumping() => animator.GetBool(_isJumping);
        public void IsJumping(bool value) => animator.SetBool(_isJumping, value);
        public bool IsClinging() => animator.GetBool(_isClinging);
        public void IsClinging(bool value) => animator.SetBool(_isClinging, value);
        public void StartJumpAnimation() => animator.SetTrigger(_startJump);
        public void StartHurtAnimation() => animator.SetTrigger(_hurt);
        public float TimeSinceLastShoot() => animator.GetFloat(_timeSinceLastShoot);
        public void TimeSinceLastShoot(float value) => animator.SetFloat(_timeSinceLastShoot, value);
        #endregion

        private void Update()
        {
            TimeSinceLastGrounded += Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (IsGrounded())
            {
                TimeSinceLastGrounded = 0;
            }
        }

        public bool IsGrounded()
        {
            Collider2D[] colliders = new Collider2D[1];
            Physics2D.OverlapAreaNonAlloc(groundCheckPoint1.position, groundCheckPoint2.position , colliders, _walkableSurfaceMask);
            if(colliders[0] == null) return false;
            else return true;
        }

        public bool IsOnWall()
        {
            Collider2D[] colliders = new Collider2D[1];
            Physics2D.OverlapAreaNonAlloc(wallCheckPoint1.position, wallCheckPoint2.position , colliders, _clingableSurfaceMask);
            if(colliders[0] == null) return false;
            else return true;
        }
    }
}
