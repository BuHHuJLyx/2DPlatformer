using UnityEngine;

[RequireComponent(typeof(Health), typeof(Attacker))]
[RequireComponent(typeof(EnemyMover), typeof(EnemyPatrol), typeof(EnemyChase))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _detectionRange = 5f;

    private bool _isChasing;

    private Health _health;
    private Attacker _attacker;

    private EnemyMover _mover;
    private EnemyPatrol _patrol;
    private EnemyChase _chase;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _attacker = GetComponent<Attacker>();
        _mover = GetComponent<EnemyMover>();
        _patrol = GetComponent<EnemyPatrol>();
        _chase = GetComponent<EnemyChase>();
    }

    private void OnEnable()
    {
        _health.Depleted += Die;
    }

    private void Update()
    {
        float distance = (_player.transform.position - transform.position).sqrMagnitude;

        float detectionDistance = Mathf.Pow(_detectionRange, 2);
        float loseDistance = Mathf.Pow(_detectionRange * 1.5f, 2);
        float attackDistance = Mathf.Pow(_attacker.AttackRange, 2);

        if (distance < detectionDistance)
            _isChasing = true;
        else if (distance > loseDistance)
            _isChasing = false;

        if (_isChasing)
        {
            if (_attacker.CanAttack && distance < attackDistance)
            {
                _attacker.Attack();
            }
            else
            {
                float direction = _chase.GetDirection(transform, _player.transform);
                _mover.Move(direction);
            }
        }
        else
        {
            float direction = _patrol.GetDirection();
            _mover.Move(direction);
        }
    }

    private void OnDisable()
    {
        _health.Depleted -= Die;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}