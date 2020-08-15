using System;
using UnityEngine;
using WarpedBounty.Pooling;

namespace WarpedBounty.Player
{
    public class PlayerWeapons : MonoBehaviour
    {
        [SerializeField] private PlayerInfo player;
        [SerializeField] private Transform firePointStand;
        [SerializeField] private Transform firePointDuck;
        [SerializeField] private Transform firePointUp;

        private float _shootTimeElapsed = 0f;

        private void Update()
        {
            _shootTimeElapsed += Time.deltaTime;
            player.TimeSinceLastShoot(_shootTimeElapsed);
        }

        public void Shoot()
        {
            Vector3 firePoint;

            if (player.IsDucking())
                firePoint = firePointDuck.position;
            else if (player.IsFacingUp())
                firePoint = firePointUp.position;
            else
                firePoint = firePointStand.position;

            var newShot = ShotPool.Instance.Get();
            newShot.transform.position = firePoint;
            newShot.SetDirection(player.DirectionFacing);
            newShot.gameObject.SetActive(true);
            _shootTimeElapsed = 0f;
        }
    }
}
