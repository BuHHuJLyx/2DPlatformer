using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerMover))]
[RequireComponent(typeof(Health), typeof(Attacker))]
[RequireComponent(typeof(Collector), typeof(Wallet))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _animator;
    
    private InputReader _input;
    private PlayerMover _mover;
    private Health _health;
    private Attacker _attacker;
    private Collector _collector;
    private Wallet _wallet;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _health = GetComponent<Health>();
        _attacker = GetComponent<Attacker>();
        _collector = GetComponent<Collector>();
        _wallet = GetComponent<Wallet>();
    }

    private void OnEnable()
    {
        _collector.CoinCollected += AddCoin;
        _collector.MedkitCollected += Heal;
        _health.Depleted += Die;
    }

    private void Update()
    {
        _animator.SetSpeed(Mathf.Abs(_mover.LinearVelocity.x));
        _animator.SetVerticalVelocity(_mover.LinearVelocity.y);
        _animator.SetGrounded(_mover.IsGround);
    }
    
    private void FixedUpdate()
    {
        _mover.Move(_input.Move);

        if (_input.GetIsJump())
            _mover.Jump();

        if (_input.GetIsAttack())
            _attacker.Attack();
    }

    private void OnDisable()
    {
        _collector.CoinCollected -= AddCoin;
        _collector.MedkitCollected -= Heal;
        _health.Depleted -= Die;
    }
    
    public void AddCoin(Coin  coin)
    {
        _wallet.Add();
    }
    
    public void Heal(Medkit  medkit)
    {
        _health.ExcuteHeal(medkit.HealAmount);
    }
    
    private void Die()
    {
        enabled = false;
        _mover.Stop();
    }
}