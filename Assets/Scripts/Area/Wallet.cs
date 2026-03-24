using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action<int> CoinCollected;

    public int Coins { get; private set; }
    
    public void Add()
    {
        Coins++;
        CoinCollected?.Invoke(Coins);
    }
}
