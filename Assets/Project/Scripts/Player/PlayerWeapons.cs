using System;
using UnityEngine;

namespace WarpedBounty.Player
{
    public class PlayerWeapons : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private readonly int a_TimeSinceLastShoot = Animator.StringToHash("TimeSinceLastShoot");

        private float _shootTimeElapsed = 0f;

        private void Start()
        {
            animator.SetFloat(a_TimeSinceLastShoot, 100f);
        }

        private void Update()
        {
            _shootTimeElapsed += Time.deltaTime;
            animator.SetFloat(a_TimeSinceLastShoot, _shootTimeElapsed);
        }

        public void Shoot()
        {
            _shootTimeElapsed = 0f;
        }
    }
}
