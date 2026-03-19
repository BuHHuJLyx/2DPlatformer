using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    private Rigidbody2D _rb;
    private Animator _anim;
    private bool _isGrounded;
    
    public event Action<int> CoinCollected;
    
    public int Coins { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _anim.SetFloat("Speed", Mathf.Abs(_rb.linearVelocity.x));
        _anim.SetFloat("YVelocity", _rb.linearVelocity.y);
        _anim.SetBool("IsGrounded", _isGrounded);

        Move();
        Jump();
    }

    public void AddCoin()
    {
        Coins++;
        CoinCollected?.Invoke(Coins);
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
        
        _rb.linearVelocity = new Vector2(_input.Move * _moveSpeed, _rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (_input.JumpPressed && _isGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
            _input.ResetJump();
        }
    }
}