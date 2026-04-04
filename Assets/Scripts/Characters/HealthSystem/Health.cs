using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue = 100;

    private int _minValue = 0;

    public event Action<int> Changed;
    public event Action Depleted;

    public int CurrentValue { get; private set; }
    public int MaxValue => _maxValue;

    public bool IsDepleted => CurrentValue <= 0;

    private void Start()
    {
        CurrentValue = _maxValue;
        Changed?.Invoke(CurrentValue);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            return;

        CurrentValue -= damage;
        CurrentValue = Mathf.Clamp(CurrentValue, _minValue, _maxValue);

        Changed?.Invoke(CurrentValue);

        if (IsDepleted)
            Depleted?.Invoke();
    }

    public void ExcuteHeal(int heal)
    {
        if (heal < 0)
            return;

        CurrentValue += heal;
        CurrentValue = Mathf.Clamp(CurrentValue, _minValue, _maxValue);

        Changed?.Invoke(CurrentValue);
    }
}