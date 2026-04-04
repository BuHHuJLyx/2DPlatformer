using System.Collections;
using UnityEngine;

public class SmoothHealthBarView : HealthBarView
{
    [SerializeField] private float _duration = 0.5f;

    private Coroutine _coroutine;

    protected override void UpdateView(int currentValue)
    {
        float targetValue = (float)currentValue / Health.MaxValue;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
        
        _coroutine = StartCoroutine(SmoothChange(targetValue));
    }

    private IEnumerator SmoothChange(float targetValue)
    {
        float start = Component.value;
        float time = 0;

        while (time < _duration)
        {
            time += Time.deltaTime;
            float normalizedTime = time / _duration;

            Component.value = Mathf.Lerp(start, targetValue, normalizedTime);
            yield return null;
        }

        Component.value = targetValue;
    }
}