using UnityEngine;

namespace WarpedBounty.Player
{
    public class PlayerInfo : MonoBehaviour
    {
        public Vector3 Direction { get; set; }
        [SerializeField] private Animator animator;
        private readonly int a_IsMoving = Animator.StringToHash("IsMoving");
        private readonly int a_IsFacingUp = Animator.StringToHash("IsFacingUp");
        private readonly int a_IsDucking = Animator.StringToHash("IsDucking");
        private readonly int a_IsJumping = Animator.StringToHash("IsJumping");
        private readonly int a_StartJump = Animator.StringToHash("StartJump");
        private readonly int a_TimeSinceLastShoot = Animator.StringToHash("TimeSinceLastShoot");

        public bool IsMoving() => animator.GetBool(a_IsMoving);
        public void IsMoving(bool value) => animator.SetBool(a_IsMoving, value);
        public bool IsFacingUp() => animator.GetBool(a_IsFacingUp);
        public void IsFacingUp(bool value) => animator.SetBool(a_IsFacingUp, value);
        public bool IsDucking() => animator.GetBool(a_IsDucking);
        public void IsDucking(bool value) => animator.SetBool(a_IsDucking, value);
        public bool IsJumping() => animator.GetBool(a_IsJumping);
        public void IsJumping(bool value) => animator.SetBool(a_IsJumping, value);
        public void StartJumpAnimation() => animator.SetTrigger(a_StartJump);
        public float TimeSinceLastShoot() => animator.GetFloat(a_TimeSinceLastShoot);
        public void TimeSinceLastShoot(float value) => animator.SetFloat(a_TimeSinceLastShoot, value);
    }
}
