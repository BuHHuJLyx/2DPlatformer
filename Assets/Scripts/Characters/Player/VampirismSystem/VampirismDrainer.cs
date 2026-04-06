using UnityEngine;

[RequireComponent(typeof(Health))]
public class VampirismDrainer : MonoBehaviour
{
    [SerializeField] private float _damagePerSecond = 10f;
    
    private Health _playerHealth;
    private float _accumulatedDamage;
    
    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
    }
    
    public void Drain(Health closestEnemy)
    {
        if (closestEnemy == null)
            return;

        _accumulatedDamage += _damagePerSecond * Time.deltaTime;
        int damage = Mathf.FloorToInt(_accumulatedDamage);
        
        if (damage > 0)
        {
            _accumulatedDamage -= damage;
            
            closestEnemy.TakeDamage(damage);
            _playerHealth.ExcuteHeal(damage);
        }
    }
}
