using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.CoinCollected += UpdateText;
    }

    private void OnDisable()
    {
        _player.CoinCollected -= UpdateText;
    }

    private void UpdateText(int coins)
    {
        _coinText.text = "Монет: " + coins;
    }
}