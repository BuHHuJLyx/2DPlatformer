using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Collector : MonoBehaviour
{
    public event Action<Coin> CoinCollected;
    public event Action<Medkit> MedkitCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Coin coin))
        {
            CoinCollected?.Invoke(coin);
            coin.Collect();
        }
        
        if (other.gameObject.TryGetComponent(out Medkit medkit))
        {
            MedkitCollected?.Invoke(medkit);
            medkit.Collect();
        }
    }
}
