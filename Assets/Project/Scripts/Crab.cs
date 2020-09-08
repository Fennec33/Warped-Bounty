using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using WarpedBounty;

public class Crab : MonoBehaviour, IDamagable
{
    [SerializeField] private float speed = 1;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private bool startFacingRight = true;
    [SerializeField] private Transform wallCheckPoint1;
    [SerializeField] private Transform wallCheckPoint2;
    
    private LayerMask _wallSurfaceMask;

    private int _currentHealth;
    private Vector3 _direction = Vector3.left;

    private void Awake()
    {
        if (startFacingRight) FlipDirection();
        _currentHealth = maxHealth;
        
        _wallSurfaceMask = LayerMask.GetMask("Wall", "Platform");
    }

    private void FixedUpdate()
    {
        transform.position += Time.deltaTime * speed * _direction;
    }

    private void FlipDirection()
    {
        _direction = _direction.Equals(Vector3.left) ? Vector3.right : Vector3.left;
        
        var t = transform;
        var v = t.localScale;
        v.x = -_direction.x;
        t.localScale = v;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Collider2D[] colliders = new Collider2D[1];
        Physics2D.OverlapAreaNonAlloc(wallCheckPoint1.position, wallCheckPoint2.position , colliders, _wallSurfaceMask);
        if(colliders[0] != null) FlipDirection();
    }

    public void Damage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
