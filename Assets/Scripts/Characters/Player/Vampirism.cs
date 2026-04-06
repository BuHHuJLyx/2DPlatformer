using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _damagePerSecond = 10f;

    [SerializeField] private GameObject _radiusSprite;
    [SerializeField] private Slider _slider;
    [SerializeField] private LayerMask _enemyMask;

    private Health _playerHealth;

    private float _duration = 6f;
    private float _cooldown = 4f;
    private float _timer;
    private float _cooldownTimer;
    private float _accumulatedDamage;

    private bool _isActive;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        if (_isActive)
        {
            _timer += Time.deltaTime;

            Drain();

            if (_timer >= _duration)
                Deactivate();

            _slider.value = 1f - (_timer / _duration);
        }
        else if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
            _slider.value = 1f - (_cooldownTimer / _cooldown);
        }
    }

    public void TryActivate()
    {
        if (_isActive || _cooldownTimer > 0)
            return;

        _isActive = true;
        _timer = 0f;

        if (_radiusSprite != null)
            _radiusSprite.SetActive(true);
    }

    private void Deactivate()
    {
        _isActive = false;
        _cooldownTimer = _cooldown;

        if (_radiusSprite != null)
            _radiusSprite.SetActive(false);
    }

    private void Drain()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyMask);

        Health closestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Health health))
            {
                float distance = (hit.transform.position - transform.position).sqrMagnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = health;
                }
            }
        }

        if (closestEnemy == null)
            return;

        _accumulatedDamage += _damagePerSecond * Time.deltaTime;
        int damage = Mathf.FloorToInt(_accumulatedDamage);
        
        Debug.Log(damage);
        
        if (damage > 0)
        {
            _accumulatedDamage -= damage;
            
            closestEnemy.TakeDamage(damage);
            _playerHealth.ExcuteHeal(damage);
        }
    }
}