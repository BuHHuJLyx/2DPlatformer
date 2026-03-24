using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    private int _minHealth = 0;

    public event Action<int> HealthChanged;
    public event Action Died;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => _maxHealth;

    public bool IsDead => CurrentHealth <= 0;

    private void Start()
    {
        CurrentHealth = _maxHealth;
        HealthChanged?.Invoke(CurrentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (IsDead)
            return;

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, _minHealth, _maxHealth);

        HealthChanged?.Invoke(CurrentHealth);

        if (IsDead)
            Died?.Invoke();
    }

    public void Heal(int heal)
    {
        if (IsDead)
            return;

        CurrentHealth += heal;
        CurrentHealth = Mathf.Clamp(CurrentHealth, _minHealth, _maxHealth);

        HealthChanged?.Invoke(CurrentHealth);
    }
}