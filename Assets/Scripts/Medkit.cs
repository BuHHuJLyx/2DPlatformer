using System;
using System.Collections;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private float _medkitLife = 15f;
    [SerializeField] private int _healAmount = 25;
    
    private WaitForSeconds _lifeDelay;
    
    public Action<Medkit> Collected;

    private void Awake()
    {
        _lifeDelay = new WaitForSeconds(_medkitLife);
    }

    public void Activate(Vector3 position)
    {
        transform.position = position;
        
        gameObject.SetActive(true);

        StartCoroutine(Life());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.Heal(_healAmount);
            Collected?.Invoke(this);
        }
    }

    private IEnumerator Life()
    {
        yield return _lifeDelay;
        Collected?.Invoke(this);
    }
}
