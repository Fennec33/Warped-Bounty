using System;
using UnityEngine;

namespace MichaelCox.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;

        private Vector3 _direction;

        public void Move(Vector3 direction)
        {
            _direction = direction;
        }

        private void FixedUpdate()
        {
            transform.position += Time.deltaTime * speed * _direction;
        }
    }
}
