using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(InputReader))]
[RequireComponent(typeof(Health), typeof(Attacker), typeof(GroundChecker))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    public static readonly int Speed = Animator.StringToHash(nameof(Speed));
    public static readonly int YVelocity = Animator.StringToHash(nameof(YVelocity));
    public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private InputReader _input;
    private Health _health;
    private Attacker _attacker;
    private GroundChecker _groundChecker;

    public event Action<int> CoinCollected;

    public int Coins { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _input = GetComponent<InputReader>();
        _health = GetComponent<Health>();
        _attacker = GetComponent<Attacker>();
        _groundChecker = GetComponent<GroundChecker>();
    }

    private void OnEnable()
    {
        _input.JumpPressed += Jump;
        _input.AttackPressed += _attacker.Attack;
        _health.Died += Die;
    }

    private void Update()
    {
        _animator.SetFloat(Speed, Mathf.Abs(_rigidbody.linearVelocity.x));
        _animator.SetFloat(YVelocity, _rigidbody.linearVelocity.y);
        _animator.SetBool(IsGrounded, _groundChecker.IsGrounded);

        Move();
    }

    private void OnDisable()
    {
        _input.JumpPressed -= Jump;
        _input.AttackPressed -= _attacker.Attack;
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
        if (_groundChecker.IsGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
        }
    }

    private void Die()
    {
        enabled = false;
        _rigidbody.linearVelocity = Vector2.zero;
    }
}