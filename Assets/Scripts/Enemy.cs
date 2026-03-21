using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _groundCheckDistance = 0.5f;
    [SerializeField] private Transform _groundDetection;
    
    [SerializeField] private Player _player;
    [SerializeField] private float _detectionRange = 5f;
    
    [SerializeField] private int _attackDamage = 20;
    [SerializeField] private float _attackRange = 3f;
    [SerializeField] private float _attackCooldown = 1f;
    
    private float _lastAttackTime;
    private float _direction = 1f;
    private bool _isChasing;
    
    private Rigidbody2D _rigidbody;
    private Health _health;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Died += Die;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, _player.transform.position);

        if (distance < _detectionRange)
            _isChasing = true;
        else if (distance > _detectionRange * 1.5f)
            _isChasing = false;
        
        if (_isChasing)
        {
            if (distance < _attackRange)
                Attack();
            else 
                Chase();
        }
        else
        {
            Patrol();
        }
    }
    
    private void OnDisable()
    {
        _health.Died -= Die;
    }

    private void Patrol()
    {
        _rigidbody.linearVelocity = new Vector2(_direction * _moveSpeed, _rigidbody.linearVelocity.y);
        
        RaycastHit2D groundInfo = Physics2D.Raycast(_groundDetection.position, Vector2.down, _groundCheckDistance);

        if (groundInfo.collider == null)
        {
            Flip();
        }
    }
    
    private void Chase()
    {
        float direction = Mathf.Sign(_player.transform.position.x - transform.position.x);

        _rigidbody.linearVelocity = new Vector2(direction * _moveSpeed, _rigidbody.linearVelocity.y);

        transform.localScale = new Vector3(direction, 1, 1);
    }
    
    private void Flip()
    {
        _direction *= -1;
        transform.localScale = new Vector3(_direction, 1, 1);
    }
    
    private void Attack()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < _attackRange)
        {
            if (Time.time >= _lastAttackTime + _attackCooldown)
            {
                _lastAttackTime = Time.time;

                if (_player.TryGetComponent<Health>(out var health))
                {
                    health.TakeDamage(_attackDamage);
                }
            }
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}