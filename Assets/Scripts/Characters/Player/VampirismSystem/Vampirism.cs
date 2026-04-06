using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TargetFinder), typeof(VampirismDrainer), typeof(VampirismView))]
public class Vampirism : MonoBehaviour
{
    private TargetFinder _targetFinder;
    private VampirismDrainer _drainer;
    private VampirismView _view;
    
    private float _duration = 6f;
    private float _cooldown = 4f;

    private bool _isActive;

    private void Awake()
    {
        _targetFinder = GetComponent<TargetFinder>();
        _drainer = GetComponent<VampirismDrainer>();
        _view = GetComponent<VampirismView>();
    }
    
    public void TryActivate()
    {
        if (_isActive)
            return;

        StartCoroutine(AbilityCycle());
    }

    private IEnumerator AbilityCycle()
    {
        _isActive = true;
        _view.SetActive(true);

        float timer = 0f;

        while (timer < _duration)
        {
            timer += Time.deltaTime;
            
            Health enemy = _targetFinder.FindClosest(transform.position);
            _drainer.Drain(enemy);
            
            _view.SetAmount(timer / _duration);
            
            yield return null;
        }
        
        _view.SetActive(false);
        
        float cooldownTimer = _cooldown;

        while (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            
            _view.SetAmount(cooldownTimer / _cooldown);
            
            yield return null;
        }
        
        _isActive = false;
    }
}