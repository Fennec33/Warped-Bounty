using System;
using UnityEngine;
using WarpedBounty.Pooling;

namespace WarpedBounty
{
    public class Shot : MonoBehaviour
    {
        [SerializeField] private float speed = 3f;

        private Vector3 _direction = Vector3.right;
        
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
