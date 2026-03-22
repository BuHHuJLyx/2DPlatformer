using UnityEngine;

[RequireComponent(typeof(Player))]
public class Collector : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Coin>(out var coin))
        {
            _player.AddCoin();
            coin.Collect();
        }
        
        if (other.gameObject.TryGetComponent<Medkit>(out var medkit))
        {
            _player.Heal(medkit.HealAmount);
            medkit.Collect();
        }
    }
}
