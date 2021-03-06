using UnityEngine;

namespace WarpedBounty.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 3f;
        [SerializeField] private int damage = 1;
        

        private Vector3 _direction = Vector3.right;
        private float _maxLifetime = 5f;
        private float _lifetime;

        private void OnEnable()
        {
            _lifetime = 0f;
        }

        private void Update()
        {
            _lifetime += Time.deltaTime;
            if (_lifetime > _maxLifetime)
                ReturnToPool();
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction.normalized;

            if (Mathf.Abs(_direction.y) > 0.5f)
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            else
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        private void FixedUpdate()
        {
            transform.position += Time.deltaTime * speed * _direction;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var collision = other.gameObject.GetComponent<IDamagable>();
            if (collision != null)
                collision.Damage(damage);
            ReturnToPool();
        }

        protected virtual void ReturnToPool()
        {
            Destroy(this.gameObject);
        }
    }
}