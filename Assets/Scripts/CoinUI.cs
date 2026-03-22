using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.CoinCollected += UpdateText;
    }

    private void OnDisable()
    {
        _wallet.CoinCollected -= UpdateText;
    }

    private void UpdateText(int coins)
    {
        _coinText.text = "Монет: " + coins;
    }
}