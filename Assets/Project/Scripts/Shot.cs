using System;
using UnityEngine;
using WarpedBounty.Pooling;

namespace WarpedBounty
{
    public class Shot : MonoBehaviour
    {
        [SerializeField] private float speed = 3f;

        private Vector3 _direction = Vector3.right;
        private float _maxLifetime = 10f;
        private float _lifetime;

        private void OnEnable()
        {
            _lifetime = 0f;
        }

        private void Update()
        {
            _lifetime += Time.deltaTime;
            if (_lifetime > _maxLifetime)
                ShotPool.Instance.ReturnToPool(this);
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction.normalized;
        }

        private void FixedUpdate()
        {
            transform.position += Time.deltaTime * speed * _direction;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            ShotPool.Instance.ReturnToPool(this);
        }
    }
}
