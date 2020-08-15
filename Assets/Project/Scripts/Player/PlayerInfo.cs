using UnityEngine;

namespace WarpedBounty.Player
{
    public class PlayerInfo : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        #region Animator Strings To Hash
        private readonly int _isMoving = Animator.StringToHash("IsMoving");
        private readonly int _isFacingUp = Animator.StringToHash("IsFacingUp");
        private readonly int _isDucking = Animator.StringToHash("IsDucking");
        private readonly int _isJumping = Animator.StringToHash("IsJumping");
        private readonly int _startJump = Animator.StringToHash("StartJump");
        private readonly int _timeSinceLastShoot = Animator.StringToHash("TimeSinceLastShoot");
        #endregion

        private Vector3 _direction;
        public Vector3 Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                if (value != Vector3.zero)
                    DirectionFacing = value;
            }
        }
        public Vector3 DirectionFacing { get; private set; }
        
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
        public float TimeSinceLastShoot() => animator.GetFloat(_timeSinceLastShoot);
        public void TimeSinceLastShoot(float value) => animator.SetFloat(_timeSinceLastShoot, value);
        #endregion
    }
}
