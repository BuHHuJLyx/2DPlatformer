using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private CoinSpawnPoint[] _spawnPoints;
    [SerializeField] private float _repeatRate = 20f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Coin> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_coinPrefab),
            actionOnGet: (coin) => SpawnCoin(coin),
            actionOnRelease: (coin) => coin.gameObject.SetActive(false),
            actionOnDestroy: (coin) => Destroy(coin.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    private void SpawnCoin(Coin coin)
    {
        CoinSpawnPoint spawnPoint = GetRandomSpawnPoint();
 
        coin.Activate(spawnPoint.Position);

        coin.Collected += ReturnCoin;
    }

    private CoinSpawnPoint GetRandomSpawnPoint()
    {
        int index = Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[index];
    }

    private void GetCoin()
    {
        _pool.Get();
    }

    private void ReturnCoin(Coin coin)
    {
        coin.Collected -= ReturnCoin;
        _pool.Release(coin);
    }

    private IEnumerator SpawnCoins()
    {
        WaitForSeconds delay = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            GetCoin();
            yield return delay;
        }
    }
}