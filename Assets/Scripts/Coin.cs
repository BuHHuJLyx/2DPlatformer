using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _coinLife = 30f;
    
    private WaitForSeconds _lifeDelay;
    
    public Action<Coin> Collected;
    public Action<Coin> Expired;

    private void Awake()
    {
        _lifeDelay = new WaitForSeconds(_coinLife);
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