using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    
    [SerializeField] private int _attackDamage = 15;
    [SerializeField] private float _attackRange = 1.5f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Health _health;
    private bool _isGrounded;
    
    public event Action<int> CoinCollected;
    
    public int Coins { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _input.JumpPressed += Jump;
        _input.AttackPressed += Attack;
        _health.Died += Die;
    }

    private void Update()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody.linearVelocity.x));
        _animator.SetFloat("YVelocity", _rigidbody.linearVelocity.y);
        _animator.SetBool("IsGrounded", _isGrounded);

        Move();
    }
    
    private void OnDisable()
    {
        _input.JumpPressed -= Jump;
        _input.AttackPressed -= Attack;
        _health.Died -= Die;
    }

    public void AddCoin()
    {
        Coins++;
        CoinCollected?.Invoke(Coins);
    }
    
    public void Heal(int amount)
    {
        _health.Heal(amount);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out _))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Platform>(out _))
        {
            _isGrounded = false;
        }
    }

    private void Move()
    {
        if (_input.Move > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (_input.Move < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        
        _rigidbody.linearVelocity = new Vector2(_input.Move * _moveSpeed, _rigidbody.linearVelocity.y);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
        }
    }

    private void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _attackRange);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out _))
            {
                if (hit.TryGetComponent<Health>(out var health))
                {
                    health.TakeDamage(_attackDamage);
                }
            }
        }
    }
    
    private void Die()
    {
        enabled = false;
        _rigidbody.linearVelocity = Vector2.zero;
    }
}