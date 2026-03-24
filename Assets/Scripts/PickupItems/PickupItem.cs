using System;
using System.Collections;
using UnityEngine;

public abstract class PickupItem<T> : MonoBehaviour where T : PickupItem<T>
{
    [SerializeField] private float _lifeTime;
    
    private WaitForSeconds _lifeDelay;
    
    public Action<T> Collected;
    public Action<T> Expired;

    private void Awake()
    {
        _lifeDelay = new WaitForSeconds(_lifeTime);
    }

    public void Activate(Vector3 position)
    {
        transform.position = position;
        
        gameObject.SetActive(true);

        StartCoroutine(DelayedReturn());
    }

    public void Collect()
    {
        Collected?.Invoke((T)this);
    }

    private IEnumerator DelayedReturn()
    {
        yield return _lifeDelay;
        Expired?.Invoke((T)this);
    }
}
