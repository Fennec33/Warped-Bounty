using System;
using UnityEngine;

namespace WarpedBounty.Player
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private Animator animator;
        #region Animator Strings To Hash
        private readonly int _isMoving = Animator.StringToHash("IsMoving");
        private readonly int _isFacingUp = Animator.StringToHash("IsFacingUp");
        private readonly int _isDucking = Animator.StringToHash("IsDucking");
        private readonly int _isJumping = Animator.StringToHash("IsJumping");
        private readonly int _startJump = Animator.StringToHash("StartJump");
        private readonly int _timeSinceLastShoot = Animator.StringToHash("TimeSinceLastShoot");
        private readonly int _hurt = Animator.StringToHash("Hurt");
        #endregion

        private const float GroundCheckRadius = 0.01f;
        private Vector3 _direction;
        public Vector3 Direction
        {
            get => _direction;
            set
            {
                _direction = value.normalized;
                if (_direction != Vector3.zero)
                    DirectionFacing = _direction;
            }
        }
        public Vector3 DirectionFacing { get; private set; }
        public float TimeSinceLastGrounded { get; private set; }
        public GameObject GroundedOn { get; private set; }

        private void Start()
        {
            DirectionFacing = Vector3.right;
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
        public void StartJumpAnimation() => animator.SetTrigger(_startJump);
        public void StartHurtAnimation() => animator.SetTrigger(_hurt);
        public float TimeSinceLastShoot() => animator.GetFloat(_timeSinceLastShoot);
        public void TimeSinceLastShoot(float value) => animator.SetFloat(_timeSinceLastShoot, value);
        #endregion

        private void Update()
        {
            if (IsGrounded())
            {
                TimeSinceLastGrounded = 0;
            }
            else
            {
                TimeSinceLastGrounded += Time.deltaTime;
            }
        }

        public bool IsGrounded()
        {
            Collider2D[] colliders = new Collider2D[2];
            Physics2D.OverlapCircleNonAlloc(groundCheckPoint.position, GroundCheckRadius, colliders);
            foreach (var collision in colliders)
            {
                if(collision == null) continue;
                if (collision.gameObject != gameObject)
                {
                    GroundedOn = collision.gameObject;
                    return true;
                }
            }

            return false;
        }
    }
}
