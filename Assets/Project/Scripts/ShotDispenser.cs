using UnityEngine;
using WarpedBounty.Pooling;

namespace WarpedBounty
{
    public class ShotDispenser : MonoBehaviour
    {
        [SerializeField] private float shootTime = 2f;
        [SerializeField] private bool shootRight = true;

        private float _shootTimeElapsed;
        void Update()
        {
            _shootTimeElapsed += Time.deltaTime;
            if (_shootTimeElapsed >= shootTime)
            {
                _shootTimeElapsed = 0f;
                Shoot();
            }
        }

        private void Shoot()
        {
            var newShot = ShotPool.Instance.Get();
            newShot.transform.position = transform.position;
            newShot.SetDirection(GetDirection());
            newShot.gameObject.SetActive(true);
        }

        private Vector3 GetDirection()
        {
            return shootRight ? Vector3.right : Vector3.left;
        }
    }
}
