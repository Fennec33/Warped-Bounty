using System;
using UnityEngine;
using WarpedBounty.Pooling;

namespace WarpedBounty.Player
{
    public class PlayerWeapons : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject shot;
        [SerializeField] private Transform firePoint_stand;
        [SerializeField] private Transform firePoint_duck;
        [SerializeField] private Transform firePoint_up;
        
        
        private readonly int a_TimeSinceLastShoot = Animator.StringToHash("TimeSinceLastShoot");
        private readonly int a_IsFacingUp = Animator.StringToHash("IsFacingUp");
        private readonly int a_IsDucking = Animator.StringToHash("IsDucking");

        private float _shootTimeElapsed = 0f;

        private void Update()
        {
            _shootTimeElapsed += Time.deltaTime;
            animator.SetFloat(a_TimeSinceLastShoot, _shootTimeElapsed);
        }

        public void Shoot()
        {
            Vector3 firePoint;

            if (animator.GetBool(a_IsDucking))
                firePoint = firePoint_duck.position;
            else if (animator.GetBool(a_IsFacingUp))
                firePoint = firePoint_up.position;
            else
                firePoint = firePoint_stand.position;

            var newShot = ShotPool.Instance.Get();
            newShot.transform.position = firePoint;
            newShot.SetDirection(Vector3.right);
            newShot.gameObject.SetActive(true);
            _shootTimeElapsed = 0f;
        }
    }
}
