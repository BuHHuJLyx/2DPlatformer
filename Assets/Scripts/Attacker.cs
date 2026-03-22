using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackCooldown;
    
    private float _lastAttackTime;
    
    public float AttackRange => _attackRange;
    
    public bool CanAttack => Time.time >= _lastAttackTime + _attackCooldown;

    public void Attack()
    {
        if (!CanAttack)
            return;

        _lastAttackTime = Time.time;
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _attackRange);

        foreach (var hit in hits)
        {
            if (hit.gameObject == gameObject)
                continue;

            if (hit.TryGetComponent<Health>(out var health))
                health.TakeDamage(_attackDamage);
        }
    }
}