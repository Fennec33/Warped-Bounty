using System;
using UnityEngine;

namespace WarpedBounty.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private PlayerInfo player;
        [SerializeField] private int maxHealth = 10;

        private int _health;

        private void Start()
        {
            _health = maxHealth;
        }

        public void Damage(int damage)
        {
            _health -= Mathf.Abs(damage);
        }

        public void Heal(int healing)
        {
            _health += Mathf.Abs(healing);
            if (_health > maxHealth)
            {
                _health = maxHealth;
            }
        }
    }
}
