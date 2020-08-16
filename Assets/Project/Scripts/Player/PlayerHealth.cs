using System;
using UnityEngine;

namespace WarpedBounty.Player
{
    public class PlayerHealth : MonoBehaviour, IDamagable
    {
        [SerializeField] private PlayerInfo player;
        [SerializeField] private int maxHealth = 10;

        private int _health;

        private void Start()
        {
            _health = maxHealth;
            Debug.Log("Health: " + _health);
        }

        public void Damage(int damage)
        {
            _health -= Mathf.Abs(damage);
            player.StartHurtAnimation();
            Debug.Log("Health: " + _health);
            if (_health <= 0)
                Destroy(gameObject);
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
