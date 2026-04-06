using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private GameObject _attackEffectPrefab;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _targetMask;
    
    [SerializeField] private int _attackDamage;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackCooldown;
    
    private float _lastAttackTime;
    
    public float AttackRange => _attackRange;
    
    public bool CanAttack => Time.time >= _lastAttackTime + _attackCooldown;

    public void Attack()
    {
        if (CanAttack == false)
            return;

        _lastAttackTime = Time.time;
        
        SpawnEffect();
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _targetMask);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Health health))
                health.TakeDamage(_attackDamage);
        }
    }

    private void SpawnEffect()
    {
        GameObject effect = Instantiate(_attackEffectPrefab, _attackPoint.position, Quaternion.identity);
    }
}