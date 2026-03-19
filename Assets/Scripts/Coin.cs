using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _coinLife = 30f;
    
    public Action<Coin> Collected;

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
            player.AddCoin();
            Collected?.Invoke(this);
        }
    }

    private IEnumerator Life()
    {
        yield return new WaitForSeconds(_coinLife);
        
        Collected?.Invoke(this);
    }
}