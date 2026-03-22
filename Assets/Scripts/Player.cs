using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerAnimator), typeof(PlayerMovement))]
[RequireComponent(typeof(Health), typeof(Attacker))]
public class Player : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    
    private InputReader _input;
    private PlayerAnimator _animator;
    private PlayerMovement _movement;
    private Health _health;
    private Attacker _attacker;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
        _animator = GetComponent<PlayerAnimator>();
        _movement = GetComponent<PlayerMovement>();
        _health = GetComponent<Health>();
        _attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        _health.Died += Die;
    }

    private void Update()
    {
        _animator.Setup(_movement.LinearVelocity, _movement.IsGround);
    }
    
    private void FixedUpdate()
    {
        _movement.Move(_input.Move);

        if (_input.GetIsJump())
            _movement.Jump();

        if (_input.GetIsAttack())
            _attacker.Attack();
    }

    private void OnDisable()
    {
        _health.Died -= Die;
    }
    
    public void AddCoin()
    {
        _wallet.Add();
    }
    
    public void Heal(int amount)
    {
        _health.Heal(amount);
    }
    
    private void Die()
    {
        enabled = false;
        _movement.Stop();
    }
}