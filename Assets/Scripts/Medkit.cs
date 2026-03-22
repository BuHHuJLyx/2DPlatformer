using System;
using System.Collections;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private float _medkitLife = 15f;
    [SerializeField] private int _healAmount = 25;
    
    private WaitForSeconds _lifeDelay;
    
    public Action<Medkit> Collected;
    public Action<Medkit> Expired;
    
    public int HealAmount => _healAmount;

    private void Awake()
    {
        _lifeDelay = new WaitForSeconds(_medkitLife);
    }

    public void Activate(Vector3 position)
    {
        transform.position = position;
        
        gameObject.SetActive(true);

        StartCoroutine(DelayedReturn());
    }

    public void Collect()
    {
        Collected?.Invoke(this);
    }

    private IEnumerator DelayedReturn()
    {
        yield return _lifeDelay;
        Expired?.Invoke(this);
    }
}
